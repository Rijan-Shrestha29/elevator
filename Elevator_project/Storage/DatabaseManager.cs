using System;
using System.Data;
using MySql.Data.MySqlClient;

namespace Elevator_project.Storage
{
    public class DatabaseManager : IDisposable
    {
        private readonly string connectionString;
        private DataSet elevatorDataSet;
        private MySqlDataAdapter logDataAdapter;
        private MySqlConnection persistentConnection;

        public DatabaseManager()
        {
            connectionString = DBConfig.ConnectionString;
            InitializeDatabase();
            InitializeDisconnectedModel();
        }

        // -------------------- DATABASE INITIALIZATION --------------------
        private void InitializeDatabase()
        {
            try
            {
                CreateDatabaseIfNotExists();
                CreateLogsTableIfNotExist();
            }
            catch (Exception ex)
            {
                throw new Exception("Database initialization failed: " + ex.Message);
            }
        }

        private static void CreateDatabaseIfNotExists()
        {
            const string masterConnStr = "Server=localhost;Port=3307;Uid=root;Pwd=Rijan@#$123;";

            using var conn = new MySqlConnection(masterConnStr);
            conn.Open();
            const string query = "CREATE DATABASE IF NOT EXISTS ElevatorSystemDB";
            using var cmd = new MySqlCommand(query, conn);
            cmd.ExecuteNonQuery();
        }

        private static void CreateLogsTableIfNotExist()
        {
            using var conn = DBConfig.GetConnection();
            conn.Open();

            const string createLogsTable = @"
                CREATE TABLE IF NOT EXISTS ElevatorLogs (
                    LogID INT AUTO_INCREMENT PRIMARY KEY,
                    Timestamp DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP,
                    Message VARCHAR(500) NOT NULL,
                    Type VARCHAR(50) NOT NULL DEFAULT 'INFO'
                );";

            using var cmd = new MySqlCommand(createLogsTable, conn);
            cmd.ExecuteNonQuery();
        }

        // -------------------- DISCONNECTED MODEL INITIALIZATION --------------------
        private void InitializeDisconnectedModel()
        {
            try
            {
                elevatorDataSet = new DataSet("ElevatorSystem");
                InitializeLogsDataAdapter();
                LoadDataFromDatabase();
            }
            catch (Exception ex)
            {
                throw new Exception("Disconnected model initialization failed: " + ex.Message);
            }
        }

        private void InitializeLogsDataAdapter()
        {
            // Create a persistent connection for the DataAdapter
            persistentConnection = new MySqlConnection(connectionString);
            persistentConnection.Open();

            // Create DataAdapter with SELECT, INSERT, UPDATE commands
            logDataAdapter = new MySqlDataAdapter("SELECT LogID, Timestamp, Message, Type FROM ElevatorLogs ORDER BY LogID", persistentConnection);

            // Configure the INSERT command
            var insertCommand = new MySqlCommand(
                @"INSERT INTO ElevatorLogs (Timestamp, Message, Type) 
                  VALUES (@Timestamp, @Message, @Type)", persistentConnection);

            insertCommand.Parameters.Add("@Timestamp", MySqlDbType.DateTime, 0, "Timestamp");
            insertCommand.Parameters.Add("@Message", MySqlDbType.VarChar, 500, "Message");
            insertCommand.Parameters.Add("@Type", MySqlDbType.VarChar, 50, "Type");

            logDataAdapter.InsertCommand = insertCommand;

            // Configure auto-increment handling
            logDataAdapter.RowUpdated += (s, e) =>
            {
                if (e.StatementType == StatementType.Insert && e.RecordsAffected > 0)
                {
                    // Get the auto-generated LogID
                    var cmd = new MySqlCommand("SELECT LAST_INSERT_ID()", persistentConnection);
                    e.Row["LogID"] = Convert.ToInt32(cmd.ExecuteScalar());
                    e.Row.AcceptChanges();
                }
            };
        }

        private void LoadDataFromDatabase()
        {
            try
            {
                // Clear existing data and reload from database
                if (elevatorDataSet.Tables.Contains("ElevatorLogs"))
                {
                    elevatorDataSet.Tables["ElevatorLogs"].Clear();
                }

                logDataAdapter.Fill(elevatorDataSet, "ElevatorLogs");
                Console.WriteLine($"Loaded {elevatorDataSet.Tables["ElevatorLogs"].Rows.Count} log entries into local cache");
            }
            catch (Exception ex)
            {
                throw new Exception("Failed to load data from database: " + ex.Message);
            }
        }

        // -------------------- DISCONNECTED LOG METHODS --------------------
        public void InsertLogDisconnected(string message, string type = "INFO")
        {
            try
            {
                // Ensure we have the Logs table
                if (!elevatorDataSet.Tables.Contains("ElevatorLogs"))
                {
                    LoadDataFromDatabase();
                }

                DataTable logsTable = elevatorDataSet.Tables["ElevatorLogs"];

                // Create new row for the log entry
                DataRow newLogRow = logsTable.NewRow();
                newLogRow["Timestamp"] = DateTime.Now;
                newLogRow["Message"] = message;
                newLogRow["Type"] = type;

                // Add to local DataTable
                logsTable.Rows.Add(newLogRow);

                // Update database using DataAdapter (DISCONNECTED MODEL)
                int recordsAffected = logDataAdapter.Update(elevatorDataSet, "ElevatorLogs");

                Console.WriteLine($"Log inserted via disconnected model: {message}. Records affected: {recordsAffected}");
            }
            catch (Exception ex)
            {
                throw new Exception($"Disconnected log insertion failed: {ex.Message}");
            }
        }

        // -------------------- BATCH OPERATIONS --------------------
        public void BatchInsertLogs(DataTable logEntries)
        {
            try
            {
                DataTable logsTable = elevatorDataSet.Tables["ElevatorLogs"];

                foreach (DataRow row in logEntries.Rows)
                {
                    DataRow newRow = logsTable.NewRow();
                    newRow.ItemArray = row.ItemArray;
                    logsTable.Rows.Add(newRow);
                }

                // Single database update for all entries (DISCONNECTED MODEL)
                int recordsAffected = logDataAdapter.Update(elevatorDataSet, "ElevatorLogs");
                Console.WriteLine($"Batch insert completed: {recordsAffected} records added");
            }
            catch (Exception ex)
            {
                throw new Exception($"Batch insert failed: {ex.Message}");
            }
        }

        // -------------------- DATA RETRIEVAL METHODS --------------------
        public DataTable GetRecentLogsDisconnected(int count = 50)
        {
            try
            {
                // Refresh data from database to get latest entries
                LoadDataFromDatabase();

                DataTable logsTable = elevatorDataSet.Tables["ElevatorLogs"];

                // Create a view with sorting and filtering
                var recentLogsView = new DataView(logsTable);
                recentLogsView.Sort = "LogID DESC";

                // Return limited results
                DataTable resultTable = recentLogsView.ToTable();

                if (resultTable.Rows.Count > count)
                {
                    DataTable limitedTable = resultTable.Clone();
                    for (int i = 0; i < count; i++)
                    {
                        limitedTable.ImportRow(resultTable.Rows[i]);
                    }
                    return limitedTable;
                }

                return resultTable;
            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to retrieve logs: {ex.Message}");
            }
        }

        public DataTable GetAllLogsDisconnected()
        {
            LoadDataFromDatabase(); // Refresh data
            return elevatorDataSet.Tables["ElevatorLogs"].Copy();
        }

        // -------------------- HYBRID APPROACH (Backward Compatibility) --------------------
        public static void InsertLog(string message, string type = "INFO")
        {
            // Original connected method for backward compatibility
            using var conn = DBConfig.GetConnection();
            conn.Open();
            const string query = @"INSERT INTO ElevatorLogs (Timestamp, Message, Type)
                                   VALUES (@Timestamp, @Message, @Type);";

            using var cmd = new MySqlCommand(query, conn);
            cmd.Parameters.AddWithValue("@Timestamp", DateTime.Now);
            cmd.Parameters.AddWithValue("@Message", message);
            cmd.Parameters.AddWithValue("@Type", type);
            cmd.ExecuteNonQuery();
        }

        // -------------------- MEMORY MANAGEMENT --------------------
        public void ClearLocalCache()
        {
            if (elevatorDataSet != null)
            {
                elevatorDataSet.Clear();
            }
            LoadDataFromDatabase(); // Reload fresh data
        }

        // -------------------- CLEAR LOGS METHODS --------------------
        public int ClearAllLogs()
        {
            try
            {
                if (!elevatorDataSet.Tables.Contains("ElevatorLogs"))
                {
                    LoadDataFromDatabase();
                }

                // Clear database using DELETE command
                using var conn = DBConfig.GetConnection();
                conn.Open();

                var deleteCommand = new MySqlCommand("DELETE FROM ElevatorLogs", conn);
                int recordsDeleted = deleteCommand.ExecuteNonQuery();

                // Clear local cache
                elevatorDataSet.Tables["ElevatorLogs"].Clear();

                Console.WriteLine($"Cleared {recordsDeleted} log entries from database");
                return recordsDeleted;
            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to clear logs: {ex.Message}");
            }
        }

        public void ClearLocalCacheOnly()
        {
            try
            {
                if (elevatorDataSet.Tables.Contains("ElevatorLogs"))
                {
                    elevatorDataSet.Tables["ElevatorLogs"].Clear();
                }
                Console.WriteLine("Local cache cleared successfully");
            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to clear local cache: {ex.Message}");
            }
        }

        public void Dispose()
        {
            logDataAdapter?.Dispose();
            elevatorDataSet?.Dispose();
            persistentConnection?.Close();
            persistentConnection?.Dispose();
        }
    }
}