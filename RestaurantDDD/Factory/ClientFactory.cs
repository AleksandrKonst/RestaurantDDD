using RestaurantConsole.Aggregate;
using RestaurantConsole.Data;

namespace RestaurantConsole.Factory;

public class ClientFactory
{
    private readonly RestaurantContext _context;

    public ClientFactory()
    {
        _context = new RestaurantContext();
    }

    public Client CreateClient(Client client)
    {
        var newClient = _context.Clients.Add(client).Entity;
        _context.SaveChanges();
        return newClient;
    }
}