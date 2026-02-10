using InventorySalesManagementSystem.Data;
using InventorySalesManagementSystem.Models;
using Microsoft.Data.Sqlite;

namespace InventorySalesManagementSystem.Repositories
{
    public class ProductRepository
    {
        public void AddProduct(Product product)
        {
            using var connection = Database.GetConnection();
            connection.Open();

            var command = connection.CreateCommand();
            command.CommandText =
            @"
                INSERT INTO Products (Name, Price, StockQuantity)
                VALUES ($name, $price, $stock);
            ";

            command.Parameters.AddWithValue("$name", product.Name);
            command.Parameters.AddWithValue("$price", product.Price);
            command.Parameters.AddWithValue("$stock", product.StockQuantity);

            command.ExecuteNonQuery();
        }

        public List<Product> GetAllProducts()
        {
            var products = new List<Product>();

            using var connection = Database.GetConnection();
            connection.Open();

            var command = connection.CreateCommand();
            command.CommandText = "SELECT * FROM Products;";

            using var reader = command.ExecuteReader();
            while (reader.Read())
            {
                products.Add(new Product
                {
                    ProductId = reader.GetInt32(0),
                    Name = reader.GetString(1),
                    Price = reader.GetDecimal(2),
                    StockQuantity = reader.GetInt32(3)
                });
            }

            return products;
        }

        public void UpdateStock(int productId, int newStock)
        {
            using var connection = Database.GetConnection();
            connection.Open();

            var command = connection.CreateCommand();
            command.CommandText =
            @"
                UPDATE Products
                SET StockQuantity = $stock
                WHERE ProductId = $id;
            ";

            command.Parameters.AddWithValue("$stock", newStock);
            command.Parameters.AddWithValue("$id", productId);

            command.ExecuteNonQuery();
        }

        public void DeleteProduct(int productId)
        {
            using var connection = Database.GetConnection();
            connection.Open();

            var command = connection.CreateCommand();
            command.CommandText =
            @"
                DELETE FROM Products
                WHERE ProductId = $id;
            ";

            command.Parameters.AddWithValue("$id", productId);
            command.ExecuteNonQuery();
        }

        public List<Product> GetLowStockProducts(int threshold)
        {
            var products = new List<Product>();

            using var connection = Database.GetConnection();
            connection.Open();

            var command = connection.CreateCommand();
            command.CommandText =
            @"
                SELECT * FROM Products
                WHERE StockQuantity <= $threshold;
            ";

            command.Parameters.AddWithValue("$threshold", threshold);

            using var reader = command.ExecuteReader();
            while (reader.Read())
            {
                products.Add(new Product
                {
                    ProductId = reader.GetInt32(0),
                    Name = reader.GetString(1),
                    Price = reader.GetDecimal(2),
                    StockQuantity = reader.GetInt32(3)
                });
            }

            return products;
        }
    }
}
