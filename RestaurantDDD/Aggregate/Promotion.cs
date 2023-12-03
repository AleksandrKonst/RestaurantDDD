using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RestaurantDDD.Aggregate;

public class Promotion
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    [Required]
    public string Name { get; set; }
    [Required]
    public DateTime DataStart { get; set; }
    [Required]
    public DateTime DataEnd { get; set; }
    [Required]
    public int ProductId { get; set; }
    public Product Product { get; set; }
}

