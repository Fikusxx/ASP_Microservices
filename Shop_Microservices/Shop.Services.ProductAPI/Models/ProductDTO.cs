
namespace Shop.Services.ProductAPI;

public class ProductDTO
{
    public int Id { get; set; }

    public string Name { get; set; }

    public double Price { get; set; }

    public string? Description { get; set; }

    public string CategoryName { get; set; }

    public string ImagePath { get; set; }
}
