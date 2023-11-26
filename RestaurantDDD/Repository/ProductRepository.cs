using RestaurantDDD.Aggregate;
using RestaurantDDD.Data;

namespace RestaurantDDD.Repository;

public class ProductRepository
{
    private readonly RestaurantContext _context;

    public ProductRepository()
    {
        _context = new RestaurantContext();
    }

    public Product GetProduct(int id)
    {
        var product = _context.Products.FirstOrDefault(c => c.Id == id);
        return product;
    }
    
    public IEnumerable<Product> GetProducts()
    {
        var products = _context.Products.ToList();
        return products;
    }
}