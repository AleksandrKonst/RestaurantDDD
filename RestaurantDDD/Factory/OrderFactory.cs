using RestaurantDDD.Aggregate;
using RestaurantDDD.Data;

namespace RestaurantDDD.Factory;

public class OrderFactory
{
    private readonly RestaurantContext _context;

    public OrderFactory()
    {
        _context = new RestaurantContext();
    }

    public Order CreateOrder(Order order)
    {
        if (order.OrderProducts.Count <= 0) return order;
        var newOrder = _context.Orders.Add(order).Entity;
        _context.SaveChanges();
        return newOrder;

    }
}