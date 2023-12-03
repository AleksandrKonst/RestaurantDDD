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
            var productFactory = new ProductFactory();
            var orderFactory = new OrderFactory();
            var clientFactory =  new ClientFactory();
            
            var productRepository = new ProductRepository();
            var orderRepository = new OrderRepository();
            var clientRepository =  new ClientRepository();

            var productService = new ProductService();
            var orderService = new OrderService();
            var clientService =  new ClientService();

            var client = clientFactory.CreateClient(new Client
            {
                Name = "Aleksandr",
                SecondName = "Konstantinov",
                TypeOfCardId = 1,
                TypeOfPayId = 1
            });
            
            var product = productFactory.CreateProduct(new Product
            {
                Name = "Fish dish",
                Detaills = "new product",
                Quantity = 5,
                Price = 1400
            });

            //Первый процесс
            var order = orderService.PlaceOrderCommand(client.Id, "Moscow", "Obrazcova", "143906", new List<Product>() { product });
            if (orderService.PayOrderCommand(order.Id))
            {
                var sum = orderService.ProcessOrderCommand(order.Id);
                Console.WriteLine($"Стоимость: {sum}");

                var status = orderService.ShipOrderCommand(order.Id);
                Console.WriteLine($"Статус: {status}");
                
                if (status != "Not found")
                {
                    if (orderService.DeliverOrderCommand(order.Id))
                    {
                        Console.WriteLine("Заказ успешно доставлен");
                    }
                    else
                    {
                        Console.WriteLine("Заказ не доставлен");
                    }
                }
            }
            else
            {
                Console.WriteLine("Ошибка оплаты");
            }
            
            //Второй процесс
            Console.WriteLine();
            if (orderService.RequestReturnCommand(order.Id, "Неверный адрес"))
            {
                
                if (orderService.ProcessReturnCommand(order.Id))
                {
                    Console.WriteLine("Возврат подтвержден");
                    if (orderService.UpdateInventoryCommand(order.Id))
                    {
                        Console.WriteLine("Колличество инвенторя подтверждено");
                    }
                    else
                    {
                        Console.WriteLine("Колличество инвенторя не изменено");
                    }
                }
                else
                {
                    Console.WriteLine("Отмена не подтверждена");
                }
            }
            else
            {
                Console.WriteLine("Ошибка отмены");
            }
            
            //Третий процесс
            Console.WriteLine();
            if (productService.LaunchPromotionCommand(product.Id, "все за пол цены", new DateTime(2023, 12, 5), new DateTime(2023, 12, 29)))
            {
                Console.WriteLine("Акция запущена");
                if (productService.SubmitReviewCommand(product.Id, client.Id, "Отличный товар"))
                {
                    Console.WriteLine("Отзыв оставлен");
                }
                else
                {
                    Console.WriteLine("Ощибка создания отзыва");
                }
            }
            else
            {
                Console.WriteLine("Ошибка запуска акции");
            }
        }
    }
}