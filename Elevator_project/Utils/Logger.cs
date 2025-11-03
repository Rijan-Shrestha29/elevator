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

        public Logger(DataGridView dgv)
        {
            grid = dgv;

            // Initialize in-memory DataTable for UI
            logTable = new DataTable();
            logTable.Columns.Add("Timestamp", typeof(string));
            logTable.Columns.Add("Type", typeof(string));
            logTable.Columns.Add("Message", typeof(string));

            grid.DataSource = logTable;

            // Initialize Database Manager
            dbManager = new DatabaseManager();
        }

        public void Log(string message, string type = "INFO")
        {
            string timestamp = DateTime.Now.ToString("HH:mm:ss");

            // Add to UI table
            logTable.Rows.Add(timestamp, type, message);

            try
            {
                // Save into database
                DatabaseManager.InsertLog(message, type);
            }
            catch (Exception ex)
            {
                // If database logging fails, show a warning in UI but don't crash
                logTable.Rows.Add(DateTime.Now.ToString("HH:mm:ss"), "ERROR",
                    $"Failed to write to DB: {ex.Message}");
            }

            // Auto-scroll to the latest log
            if (grid.Rows.Count > 0)
            {
                grid.FirstDisplayedScrollingRowIndex = grid.Rows.Count - 1;
            }
        }

        public void ShowLogs()
        {
            MessageBox.Show("Logs are displayed in the table below the control panel.\n\n" +
                          "You can see:\n" +
                          "• Button presses\n" +
                          "• Door operations (open/close)\n" +
                          "• Movement between floors\n" +
                          "• Arrival notifications\n" +
                          "• System warnings", "Elevator Log Information",
                          MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}