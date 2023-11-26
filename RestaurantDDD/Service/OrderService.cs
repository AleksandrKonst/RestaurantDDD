using RestaurantDDD.Aggregate;
using RestaurantDDD.Data;
using RestaurantDDD.Factory;

namespace RestaurantDDD.Service;

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
    
    public Order PlaceOrderCommand(int clientId, TypeOfPay typeOfPay, List<OrderProduct> products, string city,  string street, string postalCode)
    {
        var client = _context.Clients.FirstOrDefault(c => c.Id == clientId);
        var newOrder = new Order
        {
            Client = client,
            Address = new Address()
            {
                City = city,
                Street = street,
                PostalCode = postalCode
            },
            StatusOfOrderId = 1
        };
        
        newOrder.OrderProducts = products;
        
        var order = _context.Orders.Add(newOrder);
        _context.SaveChanges();
        
        return order.Entity;
    }
}