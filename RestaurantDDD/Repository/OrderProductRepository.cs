using RestaurantConsole.Aggregate;
using RestaurantConsole.Data;

namespace RestaurantConsole.Repository;

public class OrderProductRepository
{
    private readonly RestaurantContext _context;

    public OrderProductRepository()
    {
        _context = new RestaurantContext();
    }

    public IEnumerable<OrderProduct> GetOrderProduct(int id)
    {
        var orderProducts = _context.OrderProducts.Where(c => c.OrderId == id).ToList();
        return orderProducts;
    }
    
    public IEnumerable<OrderProduct> GetOrderProducts()
    {
        var orderProducts = _context.OrderProducts.ToList();
        return orderProducts;
    }
}