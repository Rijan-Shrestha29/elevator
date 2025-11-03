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
            logTable.Columns.Add("Message", typeof(string));

            grid.DataSource = logTable;

            // Initialize Database Manager
            dbManager = new DatabaseManager();
        }

        public void Log(string message, string type = "INFO")
        {
            // 1️⃣ Add to UI table
            logTable.Rows.Add(DateTime.Now.ToString("HH:mm:ss"), message);

            try
            {
                // 2️⃣ Save into database
                DatabaseManager.InsertLog(message, type);
            }
            catch (Exception ex)
            {
                // 3️⃣ If database logging fails, show a warning in UI but don't crash
                logTable.Rows.Add(DateTime.Now.ToString("HH:mm:ss"),
                    $"[LoggerError] Failed to write to DB: {ex.Message}");
            }
        }

        public static void ShowLogs()
        {
            MessageBox.Show("Logs are displayed below the control panel.", "Elevator Log");
        }
    }
}