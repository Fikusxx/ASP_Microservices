using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Shop.Services.ShoppingCartAPI;

public class Product
{
    [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
    public int Id { get; set; }

    [Required]
    public string Name { get; set; }

    [Required]
    [Range(1, 1000)]
    public double Price { get; set; }

    public string? Description { get; set; }

    public string CategoryName { get; set; }

    public string ImagePath { get; set; }
}
