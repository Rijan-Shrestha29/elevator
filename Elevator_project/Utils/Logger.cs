using System;
using System.Data;
using System.Windows.Forms;
using Elevator_project.Storage;

namespace Elevator_project.Utils
{
    public class Logger
    {
        private readonly DataGridView grid;
        private readonly DataTable logTable;
        private readonly DatabaseManager dbManager;
        private bool disconnectedModelAvailable;

        public Logger(DataGridView dgv)
        {
            grid = dgv;

            // Initialize in-memory DataTable for UI
            logTable = new DataTable();
            logTable.Columns.Add("Timestamp", typeof(string));
            logTable.Columns.Add("Type", typeof(string));
            logTable.Columns.Add("Message", typeof(string));

            grid.DataSource = logTable;

            try
            {
                // Initialize Database Manager with disconnected model
                dbManager = new DatabaseManager();
                disconnectedModelAvailable = true;
                Log("Disconnected database model initialized successfully", "SYSTEM");
            }
            catch (Exception ex)
            {
                disconnectedModelAvailable = false;
                Log($"Disconnected model failed: {ex.Message}. Using connected model fallback.", "WARNING");
            }
        }

        public void Log(string message, string type = "INFO")
        {
            string timestamp = DateTime.Now.ToString("HH:mm:ss");

            // Add to UI table
            logTable.Rows.Add(timestamp, type, message);

            try
            {
                if (disconnectedModelAvailable)
                {
                    // Save into database using DISCONNECTED MODEL
                    dbManager.InsertLogDisconnected(message, type);
                }
                else
                {
                    // Fall back to connected model
                    DatabaseManager.InsertLog(message, type);
                }
            }
            catch (Exception ex)
            {
                // If both methods fail, show error in UI but don't crash
                logTable.Rows.Add(DateTime.Now.ToString("HH:mm:ss"), "ERROR",
                    $"Database write failed: {ex.Message}");
            }

            // Auto-scroll to the latest log
            if (grid.Rows.Count > 0)
            {
                grid.FirstDisplayedScrollingRowIndex = grid.Rows.Count - 1;
            }
        }

        public void RefreshLogsFromDatabase()
        {
            try
            {
                if (!disconnectedModelAvailable)
                {
                    Log("Cannot refresh logs - disconnected model unavailable", "WARNING");
                    return;
                }

                // Clear current UI logs
                logTable.Clear();

                // Get fresh data using disconnected model
                DataTable dbLogs = dbManager.GetRecentLogsDisconnected(100);

                // Convert database format to UI format
                foreach (DataRow dbRow in dbLogs.Rows)
                {
                    logTable.Rows.Add(
                        Convert.ToDateTime(dbRow["Timestamp"]).ToString("HH:mm:ss"),
                        dbRow["Type"].ToString(),
                        dbRow["Message"].ToString()
                    );
                }

                Log($"Refreshed {dbLogs.Rows.Count} log entries from database", "SYSTEM");
            }
            catch (Exception ex)
            {
                Log($"Failed to refresh logs: {ex.Message}", "ERROR");
            }
        }

        public void ShowLogs()
        {
            string modelType = disconnectedModelAvailable ? "DISCONNECTED MODEL" : "CONNECTED MODEL (Fallback)";

            MessageBox.Show($"Logs are displayed in the table below the control panel.\n\n" +
                          $"Using {modelType}:\n" +
                          $"• Data cached locally for performance\n" +
                          $"• Batch updates to database\n" +
                          $"• Reduced database connections\n\n" +
                          $"You can see:\n" +
                          $"• Button presses\n" +
                          $"• Door operations (open/close)\n" +
                          $"• Movement between floors\n" +
                          $"• Arrival notifications\n" +
                          $"• System warnings",
                          $"Elevator Log Information - {modelType}",
                          MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        public void ClearLogsWithOptions()
        {
            try
            {
                if (!disconnectedModelAvailable)
                {
                    // Fallback to simple clear if disconnected model unavailable
                    var result = MessageBox.Show(
                        "Database connection unavailable. Clear UI logs only?",
                        "Clear UI Logs",
                        MessageBoxButtons.YesNo,
                        MessageBoxIcon.Question);

                    if (result == DialogResult.Yes)
                    {
                        logTable.Clear();
                        Log("UI logs cleared (database unavailable)", "SYSTEM");
                    }
                    return;
                }

                using (var optionsForm = new Form()
                {
                    Text = "Clear Logs",
                    Size = new Size(400, 300),
                    StartPosition = FormStartPosition.CenterParent,
                    FormBorderStyle = FormBorderStyle.FixedDialog,
                    MaximizeBox = false,
                    MinimizeBox = false,
                    BackColor = Color.White
                })
                {
                    // Title
                    var lblTitle = new Label()
                    {
                        Text = "Clear Logs Options",
                        Location = new Point(20, 20),
                        Size = new Size(350, 25),
                        Font = new Font("Segoe UI", 11F, FontStyle.Bold),
                        ForeColor = Color.FromArgb(70, 130, 180)
                    };

                    // Clear options - ONLY TWO OPTIONS
                    var rbClearAll = new RadioButton()
                    {
                        Text = "🗑️ Clear ALL logs (UI + Database)",
                        Location = new Point(30, 65),
                        Size = new Size(350, 25),
                        Checked = true,
                        Font = new Font("Segoe UI", 9F)
                    };

                    var rbClearUIOnly = new RadioButton()
                    {
                        Text = "📱 Clear UI logs only (Keep database)",
                        Location = new Point(30, 95),
                        Size = new Size(350, 25),
                        Font = new Font("Segoe UI", 9F)
                    };

                    // Warning label
                    var lblWarning = new Label()
                    {
                        Text = "⚠️ This action cannot be undone!",
                        Location = new Point(20, 135),
                        Size = new Size(350, 20),
                        Font = new Font("Segoe UI", 9F, FontStyle.Bold),
                        ForeColor = Color.Red
                    };

                    // Buttons
                    var btnClear = new Button()
                    {
                        Text = "🚮 Clear Logs",
                        Location = new Point(90, 180),
                        Size = new Size(100, 35),
                        BackColor = Color.FromArgb(220, 53, 69),
                        ForeColor = Color.White,
                        Font = new Font("Segoe UI", 9F, FontStyle.Bold),
                        DialogResult = DialogResult.OK
                    };

                    var btnCancel = new Button()
                    {
                        Text = "Cancel",
                        Location = new Point(200, 180),
                        Size = new Size(100, 35),
                        BackColor = Color.FromArgb(108, 117, 125),
                        ForeColor = Color.White,
                        Font = new Font("Segoe UI", 9F, FontStyle.Bold),
                        DialogResult = DialogResult.Cancel
                    };

                    // Add controls to form
                    optionsForm.Controls.AddRange(new Control[]
                    {
                lblTitle,
                rbClearAll,
                rbClearUIOnly,
                lblWarning,
                btnClear,
                btnCancel
                    });

                    var result = optionsForm.ShowDialog();

                    if (result == DialogResult.OK)
                    {
                        if (rbClearAll.Checked)
                        {
                            // Clear from database AND UI
                            int recordsDeleted = dbManager.ClearAllLogs();
                            logTable.Clear();
                            Log($"Cleared {recordsDeleted} logs from database and UI", "SYSTEM");

                            MessageBox.Show(
                                $"Successfully cleared {recordsDeleted} log entries from database and UI.",
                                "Logs Cleared",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Information);
                        }
                        else if (rbClearUIOnly.Checked)
                        {
                            // Clear only UI, keep database
                            dbManager.ClearLocalCacheOnly();
                            logTable.Clear();
                            Log("UI logs cleared (database preserved)", "SYSTEM");

                            MessageBox.Show(
                                "UI logs cleared successfully. Database logs preserved.",
                                "UI Logs Cleared",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Information);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Log($"Clear operation failed: {ex.Message}", "ERROR");
                MessageBox.Show(
                    $"Error clearing logs: {ex.Message}",
                    "Clear Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }


        public void Dispose()
        {
            dbManager?.Dispose();
        }
    }
}