using InventorySalesManagementSystem.Services;

var orderService = new OrderService();

Console.Write("Customer ID: ");
int customerId = int.Parse(Console.ReadLine()!);

var items = new Dictionary<int, int>();

while (true)
{
    Console.Write("Product ID (0 to finish): ");
    int productId = int.Parse(Console.ReadLine()!);
    if (productId == 0) break;

    Console.Write("Quantity: ");
    int qty = int.Parse(Console.ReadLine()!);

    items[productId] = qty;
}

orderService.CreateOrder(customerId, items);
