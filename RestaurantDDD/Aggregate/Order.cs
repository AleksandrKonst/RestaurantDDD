using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RestaurantDDD.Aggregate;

public class Order
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    
    [Required]
    public int AddressId { get; set; }
    public Address Address { get; set; }
    
    [Required]
    public int StatusOfOrderId { get; set; }
    public StatusOfOrder StatusOfOrder { get; set; }
    
    [Required]
    public int ClientId { get; set; }
    public Client Client { get; set; }
    
    public ICollection<OrderProduct> OrderProducts { get; set; }
}

public class StatusOfOrder
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    [Required]
    public string Name { get; set; }
    
    public ICollection<Order> Orders { get; set; }
}

public class Address
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    [Required]
    public string City { get; set; }
    [Required]
    public string Street { get; set; }
    [Required]
    public string PostalCode { get; set; }
    
    public ICollection<Order> Orders { get; set; }
}

public class OrderProduct
{
    public int OrderId { get; set; }
    public Order Order { get; set; }

    public int ProductId { get; set; }
    public Product Product { get; set; }

    public int Quantity { get; set; }
}