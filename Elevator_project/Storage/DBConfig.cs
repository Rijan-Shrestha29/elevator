using MySql.Data.MySqlClient;

namespace Elevator_project.Storage  // Updated namespace
{
    public static class DBConfig
    {
        // Adjusted connection string to match your environment and logger simplicity
        public static string ConnectionString { get; } =
            "Server=localhost;Port=3307;Database=ElevatorSystemDB;Uid=root;Pwd=Rijan@#$123;";

        public static MySqlConnection GetConnection()
        {
            return new MySqlConnection(ConnectionString);
        }
    }
}