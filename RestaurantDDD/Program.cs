using RestaurantDDD.Aggregate;
using RestaurantDDD.Factory;
using RestaurantDDD.Repository;
using RestaurantDDD.Service;

namespace RestaurantDDD 
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var productRepository = new ProductRepository();
            var orderRepository = new OrderRepository();
            var orderProductRepository = new OrderProductRepository();
            var clientRepository =  new ClientRepository();

            var productFactory = new ProductFactory();
            var product = new Product()
            {
                Id = 55,
                Name = "Fish dish",
                Detaills = "new product"
            };
            productFactory.CreateProduct(product);

            var clientFactory = new ClientFactory();
            var client = new Client()
            {
                Id = 55,
                Name = "Дебетовая",
                SecondName = "Картой",
                TypeOfCardId = 1,
                TypeOfPayId = 1
            };
            clientFactory.CreateClient(client);

            var orderService = new OrderService();
            var order = new Order()
            {
                Id = 55,
                StatusOfOrderId = 1,
                AddressId = 1
            };
            var newOrder = orderService.CreateOrder(order, new List<Product>() { product }, client, 1);

            var clientService = new ClientService();
            
            Console.WriteLine($"Пользователь: {client.Name}, Number: {client.SecondName}");
            Console.WriteLine($"Продукт: {product.Name}, Описание: {product.Detaills}");
            Console.WriteLine($"Создан заказ с ID: {newOrder.Id}, Адрес доставки: {newOrder.AddressId} {newOrder.StatusOfOrderId}");
        }
    }
}