using RestaurantDDD.Aggregate;
using RestaurantDDD.Data;

namespace RestaurantDDD.Repository;

public class OrderRepository
{
    private readonly RestaurantContext _context;

    public OrderRepository()
    {
        _context = new RestaurantContext();
    }

    public Order GetOrder(int id)
    {
        var order = _context.Orders.FirstOrDefault(c => c.Id == id);
        return order;
    }
    
    public IEnumerable<Order> GetOrders()
    {
        var orders = _context.Orders.ToList();
        return orders;
    }
}