using RestaurantConsole.Aggregate;
using RestaurantConsole.Data;

namespace RestaurantConsole.Factory;

public class OrderFactory
{
    private readonly RestaurantContext _context;

    public OrderFactory()
    {
        _context = new RestaurantContext();
    }

    public Order CreateOrder(Order order)
    {
        if (order.OrderProducts.Count > 0)
        {
            var newOrder = _context.Orders.Add(order).Entity;
            _context.SaveChanges();
            return newOrder;
        }

        return order;
    }
}