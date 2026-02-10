using InventorySalesManagementSystem.Data;
using InventorySalesManagementSystem.Models;

namespace InventorySalesManagementSystem.Repositories
{
    public class OrderRepository
    {
        public int CreateOrder(Order order)
        {
            using var connection = Database.GetConnection();
            connection.Open();

            var command = connection.CreateCommand();
            command.CommandText =
            @"
                INSERT INTO Orders (CustomerId, OrderDate, TotalAmount)
                VALUES ($customerId, $date, $total);
                SELECT last_insert_rowid();
            ";

            command.Parameters.AddWithValue("$customerId", order.CustomerId);
            command.Parameters.AddWithValue("$date", order.OrderDate.ToString("s"));
            command.Parameters.AddWithValue("$total", order.TotalAmount);

            return Convert.ToInt32(command.ExecuteScalar());
        }

        public void AddOrderItem(OrderItem item)
        {
            using var connection = Database.GetConnection();
            connection.Open();

            var command = connection.CreateCommand();
            command.CommandText =
            @"
                INSERT INTO OrderItems (OrderId, ProductId, Quantity)
                VALUES ($orderId, $productId, $quantity);
            ";

            command.Parameters.AddWithValue("$orderId", item.OrderId);
            command.Parameters.AddWithValue("$productId", item.ProductId);
            command.Parameters.AddWithValue("$quantity", item.Quantity);

            command.ExecuteNonQuery();
        }
    }
}
