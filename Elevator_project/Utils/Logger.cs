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

        public void ClearAllLogs()
        {
            try
            {
                // Show confirmation dialog
                var result = MessageBox.Show(
                    "Are you sure you want to clear all logs?\nThis action cannot be undone.",
                    "Confirm Clear Logs",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Warning,
                    MessageBoxDefaultButton.Button2);

                if (result == DialogResult.Yes)
                {
                    if (disconnectedModelAvailable && dbManager != null)
                    {
                        // Clear from database
                        int recordsDeleted = dbManager.ClearAllLogs();

                        // Clear from UI
                        logTable.Clear();

                        // Add confirmation message
                        Log($"All logs cleared successfully. {recordsDeleted} records deleted.", "SYSTEM");

                        MessageBox.Show(
                            $"Successfully cleared {recordsDeleted} log entries from database and UI.",
                            "Logs Cleared",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Information);
                    }
                    else
                    {
                        // Fallback: Clear only UI
                        logTable.Clear();
                        Log("UI logs cleared (database unavailable)", "SYSTEM");

                        MessageBox.Show(
                            "UI logs cleared. Database logs could not be cleared due to connection issues.",
                            "Partial Clear",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Warning);
                    }
                }
            }
            catch (Exception ex)
            {
                Log($"Failed to clear logs: {ex.Message}", "ERROR");
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