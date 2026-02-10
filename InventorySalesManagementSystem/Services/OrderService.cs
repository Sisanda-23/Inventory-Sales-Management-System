using InventorySalesManagementSystem.Models;
using InventorySalesManagementSystem.Repositories;

namespace InventorySalesManagementSystem.Services
{
    public class OrderService
    {
        private readonly ProductRepository _productRepo;
        private readonly OrderRepository _orderRepo;

        public OrderService()
        {
            _productRepo = new ProductRepository();
            _orderRepo = new OrderRepository();
        }

        public void CreateOrder(int customerId, Dictionary<int, int> productQuantities)
        {
            decimal total = 0;

            foreach (var item in productQuantities)
            {
                var product = _productRepo
                    .GetAllProducts()
                    .FirstOrDefault(p => p.ProductId == item.Key);

                if (product == null || product.StockQuantity < item.Value)
                {
                    Console.WriteLine($"Insufficient stock for product ID {item.Key}");
                    return;
                }

                total += product.Price * item.Value;
            }

            var order = new Order
            {
                CustomerId = customerId,
                OrderDate = DateTime.Now,
                TotalAmount = total
            };

            int orderId = _orderRepo.CreateOrder(order);

            foreach (var item in productQuantities)
            {
                _orderRepo.AddOrderItem(new OrderItem
                {
                    OrderId = orderId,
                    ProductId = item.Key,
                    Quantity = item.Value
                });

                var product = _productRepo
                    .GetAllProducts()
                    .First(p => p.ProductId == item.Key);

                _productRepo.UpdateStock(
                    product.ProductId,
                    product.StockQuantity - item.Value
                );
            }

            Console.WriteLine($"Order created successfully. Total: R{total}");
        }
    }
}
