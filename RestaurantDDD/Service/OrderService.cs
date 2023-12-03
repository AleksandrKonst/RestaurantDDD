using Microsoft.EntityFrameworkCore;
using RestaurantDDD.Aggregate;
using RestaurantDDD.Data;
using RestaurantDDD.Factory;

namespace RestaurantDDD.Service;

public class OrderService
{
    private readonly RestaurantContext _context = new();
    private readonly OrderFactory _orderFactory = new();
    private readonly OrderProductFactory _orderProductFactory = new();

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
    
    //1
    public Order PlaceOrderCommand(int clientId, string city,  string street, string postalCode, List<Product> products)
    {
        //Предварительный заказ
        
        var client = _context.Clients.FirstOrDefault(c => c.Id == clientId);
        if (client != null)
        {
            var newOrder = new Order
            {
                ClientId = clientId,
                Client = client,
                Address = new Address()
                {
                    City = city,
                    Street = street,
                    PostalCode = postalCode
                },
                StatusOfOrderId = 1
            };
            
            var order = _context.Orders.Add(newOrder).Entity;
            _context.SaveChanges();
            
            foreach (var product in products)
            {
                if (product.Quantity > 0)
                {
                    product.Quantity--;
                    var productDb = _context.OrderProducts.FirstOrDefault(o => o.ProductId == product.Id && o.OrderId == order.Id);
                    if (productDb == null)
                    {
                        _context.OrderProducts.Add(new OrderProduct()
                        {
                            OrderId = order.Id,
                            ProductId = product.Id,
                            Quantity = 1
                        }); 
                    }
                    else
                    {
                        productDb.Quantity++;
                    }
                    
                }
                else
                {
                    Console.WriteLine("Товара не в наличии");
                }
            }
        
            _context.SaveChanges();
            return order;
        }

        throw new Exception("Такого клиента не существует");
    }
    
    //1
    public bool PayOrderCommand(int orderId)
    {
        var order = _context.Orders.FirstOrDefault(o => o.Id == orderId);

        if (order != null)
        {
            //Успешная оплата
            
            order.StatusOfOrderId = 2;
            _context.SaveChanges();

            return true;
        }
        
        //Ошибка оплаты
        return false;
    }
    
    //1
    public decimal ProcessOrderCommand(int orderId)
    {
        //Стоимость и Подтверждение заказа
        
        var order = _context.Orders
            .Include(o => o.OrderProducts)
                .ThenInclude(orderProduct => orderProduct.Product)
            .FirstOrDefault(o => o.Id == orderId);
        var sum = order.OrderProducts.Sum(product => product.Product.Price);
        
        order.StatusOfOrderId = 3;
        _context.SaveChanges();
        
        return sum;
    }
    
    //1
    public string ShipOrderCommand(int orderId)
    {
        //Доставка
        
        var order = _context.Orders
            .Include(o => o.StatusOfOrder)
            .FirstOrDefault(o => o.Id == orderId);
        if (order != null)
        {
            order.StatusOfOrderId = 4;
            _context.SaveChanges();

            return "Подтвержден";
        }
        return "Not found";
    }
    
    //1
    public bool DeliverOrderCommand(int orderId)
    {
        //Окончание оформления заказа
        
        var order = _context.Orders.FirstOrDefault(o => o.Id == orderId);
        if (order != null)
        {
            order.StatusOfOrderId = 5;
            _context.SaveChanges();

            return true;
        }
        return false;
    }
    
    //2
    public bool RequestReturnCommand(int orderId, string details)
    {
        //Отмена заказа
        
        var order = _context.Orders
            .Include(o => o.StatusOfOrder)
            .FirstOrDefault(o => o.Id == orderId);
        
        if (order != null)
        {
            order.StatusOfOrderId = 6;
            order.StatusOfOrder.Details = details;
            _context.SaveChanges();

            return true;
        }
        return false;
    }
    
    //2
    public bool ProcessReturnCommand(int orderId)
    {
        //Подтверждение отмены заказа
        
        var order = _context.Orders
            .Include(o => o.StatusOfOrder)
            .FirstOrDefault(o => o.Id == orderId);
        
        if (order != null && order.StatusOfOrderId == 6)
        {
            return true;
        }
        return false;
    }
    
    //2
    public bool UpdateInventoryCommand(int orderId)
    {
        //Менеджер инвентаря обновляет доступность товара
        
        var order = _context.Orders
            .Include(o => o.OrderProducts)
            .ThenInclude(product => product.Product)
            .FirstOrDefault(o => o.Id == orderId);
        
        foreach (var product in order.OrderProducts)
        {
            product.Quantity++;
        }
        
        _context.SaveChanges();
        return true;
    }
}