using RestaurantConsole.Aggregate;
using RestaurantConsole.Data;

namespace RestaurantConsole.Factory;

public class ProductFactory
{
    private readonly RestaurantContext _context;

    public ProductFactory()
    {
        _context = new RestaurantContext();
    }

    public Product CreateProduct(Product product)
    {
        var newProduct = _context.Products.Add(product).Entity;
        _context.SaveChanges();
        return newProduct;
    }
}