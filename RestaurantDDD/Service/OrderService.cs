using RestaurantConsole.Aggregate;
using RestaurantConsole.Data;
using RestaurantConsole.Factory;

namespace RestaurantConsole.Service;

public class OrderService
{
    private readonly RestaurantContext _context;
    private readonly OrderFactory _orderFactory;
    private readonly OrderProductFactory _orderProductFactory;

    public OrderService()
    {
        _context = new RestaurantContext();
        _orderFactory = new OrderFactory();
        _orderProductFactory = new OrderProductFactory();
    }

    public Order CreateOrder(Order order, List<Product> products, Client client)
    {
        foreach (var prod in products)
        {
            _context.OrderProducts.Add(new OrderProduct()
            {
                ProductId = prod.Id,
                OrderId = order.Id,
                Quantity = 1
            });
        }
        order.ClientId = client.Id;
        var newOrder = _context.Orders.Add(order).Entity;
        _context.SaveChanges();
        
        return newOrder;
    }
}