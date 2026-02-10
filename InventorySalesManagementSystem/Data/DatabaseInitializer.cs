using Microsoft.Data.Sqlite;

namespace InventorySalesManagementSystem.Data
{
    public static class DatabaseInitializer
    {
        public static void Initialize()
        {
            using var connection = Database.GetConnection();
            connection.Open();

            var command = connection.CreateCommand();
            command.CommandText =
            @"
            CREATE TABLE IF NOT EXISTS Customers (
                CustomerId INTEGER PRIMARY KEY AUTOINCREMENT,
                Name TEXT NOT NULL,
                Email TEXT NOT NULL,
                Phone TEXT
            );

            CREATE TABLE IF NOT EXISTS Products (
                ProductId INTEGER PRIMARY KEY AUTOINCREMENT,
                Name TEXT NOT NULL,
                Price REAL NOT NULL,
                StockQuantity INTEGER NOT NULL
            );

            CREATE TABLE IF NOT EXISTS Orders (
                OrderId INTEGER PRIMARY KEY AUTOINCREMENT,
                CustomerId INTEGER NOT NULL,
                OrderDate TEXT NOT NULL,
                TotalAmount REAL NOT NULL,
                FOREIGN KEY (CustomerId) REFERENCES Customers(CustomerId)
            );

            CREATE TABLE IF NOT EXISTS OrderItems (
                OrderItemId INTEGER PRIMARY KEY AUTOINCREMENT,
                OrderId INTEGER NOT NULL,
                ProductId INTEGER NOT NULL,
                Quantity INTEGER NOT NULL,
                FOREIGN KEY (OrderId) REFERENCES Orders(OrderId),
                FOREIGN KEY (ProductId) REFERENCES Products(ProductId)
            );
            ";

            command.ExecuteNonQuery();
        }
    }
}
