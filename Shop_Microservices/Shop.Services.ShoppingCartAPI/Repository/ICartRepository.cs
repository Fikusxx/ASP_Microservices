namespace Shop.Services.ShoppingCartAPI;

public interface ICartRepository
{
    public Task<CartDTO> GetCartById(int id);

    public Task<CartDTO> CreateUpdateCart(CartDTO cartDTO);

    public Task<bool> RemoveFromCart(int cartDetailsId);

    public Task<bool> ClearCart(int id);

    public Task<bool> ApplyCoupon(int id, string code);
    public Task<bool> RemoveCoupon(int id);
}
