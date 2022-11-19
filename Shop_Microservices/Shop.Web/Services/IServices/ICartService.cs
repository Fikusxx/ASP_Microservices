namespace Shop.Web;

public interface ICartService
{
    public Task<T> GetCartByIdAsync<T>(int id);

    public Task<T> AddToCartAsync<T>(CartDTO cartDTO);

    public Task<T> UpdateCartAsync<T>(CartDTO cartDTO);

    public Task<T> RemoveFromCartAsync<T>(int cartId);

    public Task<T> ApplyCoupon<T>(CartDTO cartDTO);

    public Task<T> RemoveCoupon<T>(int id);

    public Task<T> Checkout<T>(CartHeaderDTO cartHeaderDTO);
}
