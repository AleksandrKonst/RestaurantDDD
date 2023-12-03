using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RestaurantDDD.Aggregate;

public class Client
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    [Required]
    public string Name { get; set; }
    [Required]
    public string SecondName { get; set; }
    
    [Required]
    public int TypeOfCardId { get; set; }
    public TypeOfCard TypeOfCard { get; set; }
    
    [Required]
    public int TypeOfPayId { get; set; }
    public TypeOfPay TypeOfPay { get; set; }
    
    public ICollection<Order> Orders { get; set; }
    
    public ICollection<ClientReview> ClientReviews { get; set; }
}

public class TypeOfPay
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    [Required]
    public string Name { get; set; }
    
    public ICollection<Client> Clients { get; set; }
}

public class TypeOfCard
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    [Required]
    public string Name { get; set; }
    
    public ICollection<Client> Clients { get; set; }
}