using InventorySalesManagementSystem.Data;
using InventorySalesManagementSystem.Models;

namespace InventorySalesManagementSystem.Repositories
{
    public class CustomerRepository
    {
        public void AddCustomer(Customer customer)
        {
            using var connection = Database.GetConnection();
            connection.Open();

            var command = connection.CreateCommand();
            command.CommandText =
            @"
                INSERT INTO Customers (Name, Email, Phone)
                VALUES ($name, $email, $phone);
            ";

            command.Parameters.AddWithValue("$name", customer.Name);
            command.Parameters.AddWithValue("$email", customer.Email);
            command.Parameters.AddWithValue("$phone", customer.Phone);

            command.ExecuteNonQuery();
        }

        public List<Customer> GetAllCustomers()
        {
            var customers = new List<Customer>();

            using var connection = Database.GetConnection();
            connection.Open();

            var command = connection.CreateCommand();
            command.CommandText = "SELECT * FROM Customers;";

            using var reader = command.ExecuteReader();
            while (reader.Read())
            {
                customers.Add(new Customer
                {
                    CustomerId = reader.GetInt32(0),
                    Name = reader.GetString(1),
                    Email = reader.GetString(2),
                    Phone = reader.IsDBNull(3) ? "" : reader.GetString(3)
                });
            }

            return customers;
        }

        public List<Customer> SearchCustomers(string keyword)
        {
            var customers = new List<Customer>();

            using var connection = Database.GetConnection();
            connection.Open();

            var command = connection.CreateCommand();
            command.CommandText =
            @"
                SELECT * FROM Customers
                WHERE Name LIKE $keyword OR Email LIKE $keyword;
            ";

            command.Parameters.AddWithValue("$keyword", $"%{keyword}%");

            using var reader = command.ExecuteReader();
            while (reader.Read())
            {
                customers.Add(new Customer
                {
                    CustomerId = reader.GetInt32(0),
                    Name = reader.GetString(1),
                    Email = reader.GetString(2),
                    Phone = reader.IsDBNull(3) ? "" : reader.GetString(3)
                });
            }

            return customers;
        }

        public void DeleteCustomer(int customerId)
        {
            using var connection = Database.GetConnection();
            connection.Open();

            var command = connection.CreateCommand();
            command.CommandText =
            @"
                DELETE FROM Customers
                WHERE CustomerId = $id;
            ";

            command.Parameters.AddWithValue("$id", customerId);
            command.ExecuteNonQuery();
        }
    }
}
