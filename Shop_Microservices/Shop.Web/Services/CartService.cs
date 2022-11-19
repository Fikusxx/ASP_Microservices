namespace Shop.Web;

public class CartService : BaseService, ICartService
{
    public CartService(IHttpClientFactory HttpClientFactory) : base(HttpClientFactory)
    { }

    public async Task<T> AddToCartAsync<T>(CartDTO cartDTO)
    {
        var apiRequest = new ApiRequest()
        {
            ApiType = StaticDetails.ApiType.POST,
            Data = cartDTO,
            Url = StaticDetails.ShoppingCartAPIBase + "api/cart/AddCart",
            AccessToken = ""
        };

        return await SendAsync<T>(apiRequest);
    }

    public async Task<T> ApplyCoupon<T>(CartDTO cartDTO)
    {
        var apiRequest = new ApiRequest()
        {
            ApiType = StaticDetails.ApiType.POST,
            Data = cartDTO,
            Url = StaticDetails.ShoppingCartAPIBase + "api/cart/ApplyCoupon",
            AccessToken = ""
        };

        return await SendAsync<T>(apiRequest);
    }

    public async Task<T> RemoveCoupon<T>(int id)
    {
        var apiRequest = new ApiRequest()
        {
            ApiType = StaticDetails.ApiType.POST,
            Data = id,
            Url = StaticDetails.ShoppingCartAPIBase + "api/cart/RemoveCoupon",
            AccessToken = ""
        };

        return await SendAsync<T>(apiRequest);
    }

    public async Task<T> GetCartByIdAsync<T>(int id)
    {
        var apiRequest = new ApiRequest()
        {
            ApiType = StaticDetails.ApiType.GET,
            Url = StaticDetails.ShoppingCartAPIBase + "api/cart/GetCart/" + id,
            AccessToken = ""
        };

        return await SendAsync<T>(apiRequest);
    }

    public async Task<T> RemoveFromCartAsync<T>(int cartId)
    {
        var apiRequest = new ApiRequest()
        {
            ApiType = StaticDetails.ApiType.POST,
            Data = cartId,
            Url = StaticDetails.ShoppingCartAPIBase + "api/cart/RemoveCart",
            AccessToken = ""
        };

        return await SendAsync<T>(apiRequest);
    }

    public async Task<T> UpdateCartAsync<T>(CartDTO cartDTO)
    {
        var apiRequest = new ApiRequest()
        {
            ApiType = StaticDetails.ApiType.POST,
            Data = cartDTO,
            Url = StaticDetails.ShoppingCartAPIBase + "api/cart/UpdateCart",
            AccessToken = ""
        };

        return await SendAsync<T>(apiRequest);
    }

    public async Task<T> Checkout<T>(CartHeaderDTO cartHeaderDTO)
    {
        var apiRequest = new ApiRequest()
        {
            ApiType = StaticDetails.ApiType.POST,
            Data = cartHeaderDTO,
            Url = StaticDetails.ShoppingCartAPIBase + "api/cart/Checkout",
            AccessToken = ""
        };

        return await SendAsync<T>(apiRequest);
    }
}
