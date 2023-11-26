using RestaurantConsole.Data;

namespace RestaurantConsole.Service;

public class ClientService
{
    private readonly RestaurantContext _context;

    public ClientService()
    {
        _context = new RestaurantContext();
    }
}