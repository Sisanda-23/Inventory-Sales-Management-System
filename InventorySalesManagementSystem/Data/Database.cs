using Microsoft.Data.Sqlite;

namespace InventorySalesManagementSystem.Data
{
    public static class Database
    {
        private static readonly string connectionString = "Data Source=inventory.db";

        public static SqliteConnection GetConnection()
        {
            return new SqliteConnection(connectionString);
        }
    }
}
