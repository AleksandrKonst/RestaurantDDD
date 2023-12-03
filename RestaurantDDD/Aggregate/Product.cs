using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RestaurantDDD.Aggregate;

public class Product
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    [Required]
    public string Name { get; set; }
    [Required]
    public string Detaills { get; set; }
    [Required]
    public decimal Price { get; set; }
    [Required]
    public int Quantity { get; set; }
    
    public ICollection<OrderProduct> OrderProducts { get; set; }
    
    public ICollection<Promotion> Promotions { get; set; }
    
    public ICollection<ClientReview> ClientReviews { get; set; }
}

public class ClientReview
{
    [Required]
    public int ProductId { get; set; }
    public Product Product { get; set; }
    [Required]
    public int ClientId { get; set; }
    public Client Client { get; set; }
    [Required]
    public string Review { get; set; }
}