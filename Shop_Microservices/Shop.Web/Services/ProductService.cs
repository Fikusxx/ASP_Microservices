namespace Shop.Web;

public class ProductService : BaseService, IProductService
{
    public ProductService(IHttpClientFactory HttpClientFactory) : base(HttpClientFactory)
    { }

    public async Task<T> GetProductByIdAsync<T>(int id)
    {
        var apiRequest = new ApiRequest()
        {
            ApiType = StaticDetails.ApiType.GET,
            Url = StaticDetails.ProductAPIBase + "api/products/" + id,
            AccessToken = ""
        };

        return await SendAsync<T>(apiRequest);
    }

    public async Task<T> GetAllProductsAsync<T>()
    {
        var apiRequest = new ApiRequest()
        {
            ApiType = StaticDetails.ApiType.GET,
            Url = StaticDetails.ProductAPIBase + "api/products/",
            AccessToken = ""
        };

        return await SendAsync<T>(apiRequest);
    }

    public async Task<T> CreateProductAsync<T>(ProductDTO productDTO)
    {
        var apiRequest = new ApiRequest()
        {
            ApiType = StaticDetails.ApiType.POST,
            Data = productDTO,
            Url = StaticDetails.ProductAPIBase + "api/products",
            AccessToken = ""
        };

        return await SendAsync<T>(apiRequest);
    }

    public async Task<T> UpdateProductAsync<T>(ProductDTO productDTO)
    {
        var apiRequest = new ApiRequest()
        {
            ApiType = StaticDetails.ApiType.PUT,
            Data = productDTO,
            Url = StaticDetails.ProductAPIBase + "api/products/",
            AccessToken = ""
        };

        return await SendAsync<T>(apiRequest);
    }

    public async Task<T> DeleteProductAsync<T>(int id)
    {
        var apiRequest = new ApiRequest()
        {
            ApiType = StaticDetails.ApiType.DELETE,
            Url = StaticDetails.ProductAPIBase + "api/products/" + id,
            AccessToken = ""
        };

        return await SendAsync<T>(apiRequest);
    }
}
