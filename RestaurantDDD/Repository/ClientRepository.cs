using RestaurantConsole.Aggregate;
using RestaurantConsole.Data;

namespace RestaurantConsole.Repository;

public class ClientRepository
{
    private readonly RestaurantContext _context;

    public ClientRepository()
    {
        _context = new RestaurantContext();
    }

    public Client GetClient(int id)
    {
        var client = _context.Clients.FirstOrDefault(c => c.Id == id);
        return client;
    }
    
    public IEnumerable<Client> GetClients()
    {
        var clients = _context.Clients.ToList();
        return clients;
    }
}