using RestaurantDDD.Aggregate;
using RestaurantDDD.Data;
using RestaurantDDD.Factory;

namespace RestaurantDDD.Service;

public class ProductService
{
    private readonly RestaurantContext _context = new();
    private readonly OrderFactory _orderFactory = new();
    private readonly OrderProductFactory _orderProductFactory = new();
    
    //3
    public bool LaunchPromotionCommand(int productId, string name, DateTime dataStart, DateTime dataEnd)
    {
        //Запуск акции
        
        var product = _context.Products.FirstOrDefault(p => p.Id == productId);
        if (product != null)
        {
            var promotion = new Promotion
            {
                Name = name,
                DataStart = dataStart.ToUniversalTime(),
                DataEnd = dataEnd.ToUniversalTime(),
                ProductId = productId
            };
            _context.Promotions.Add(promotion);
            _context.SaveChanges();

            return true;
        }
        return false;
    }
    
    //3
    public bool SubmitReviewCommand(int productId, int clientId, string review)
    {
        //Отзыв
        
        var product = _context.Products.FirstOrDefault(p => p.Id == productId);
        var client = _context.Clients.FirstOrDefault(c => c.Id == clientId);
        if (product != null && client != null)
        {
            _context.ClientReviews.Add(new ClientReview
            {
                ClientId = clientId,
                ProductId = productId,
                Review = review
            });
            _context.SaveChanges();

            return true;
        }
        return false;
    }
}