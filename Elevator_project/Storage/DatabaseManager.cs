using System;
using System.Data;
using MySql.Data.MySqlClient;

namespace Elevator_project.Storage
{
    public class DatabaseManager
    {
        private readonly string connectionString;

        public DatabaseManager()
        {
            connectionString = DBConfig.ConnectionString;
            InitializeDatabase();
        }

        // -------------------- INITIALIZATION --------------------
        private static void InitializeDatabase()
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

        // -------------------- LOG METHODS --------------------
        public static void InsertLog(string message, string type = "INFO")
        {
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

        public static DataTable GetRecentLogs(int count = 50)
        {
            var dt = new DataTable();
            using var conn = DBConfig.GetConnection();
            conn.Open();
            string query = $@"
                SELECT Timestamp, Message, Type
                FROM ElevatorLogs
                ORDER BY LogID DESC
                LIMIT {count};";

            using var cmd = new MySqlCommand(query, conn);
            using var adapter = new MySqlDataAdapter(cmd);
            adapter.Fill(dt);

            return dt;
        }

        public static DataTable GetAllLogs()
        {
            var dt = new DataTable();
            using var conn = DBConfig.GetConnection();
            conn.Open();
            const string query = "SELECT * FROM ElevatorLogs ORDER BY LogID ASC;";
            using var cmd = new MySqlCommand(query, conn);
            using var adapter = new MySqlDataAdapter(cmd);
            adapter.Fill(dt);

            return dt;
        }
    }
}