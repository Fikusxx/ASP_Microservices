namespace Shop.Services.ShoppingCartAPI;

public class CartDetails
{
    public int Id { get; set; }

    public int CartHeaderId { get; set; }
    
    public virtual CartHeader CartHeader { get; set; }

    public int ProductId { get; set; }

    public virtual Product Product { get; set; }

    public int Count { get; set; }
}
