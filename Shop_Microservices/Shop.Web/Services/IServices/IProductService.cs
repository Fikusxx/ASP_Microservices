namespace Shop.Web;

public interface IProductService : IBaseService
{
    public Task<T> GetAllProductsAsync<T>();

    public Task<T> GetProductByIdAsync<T>(int id);

    public Task<T> CreateProductAsync<T>(ProductDTO productDTO);

    public Task<T> UpdateProductAsync<T>(ProductDTO productDTO);

    public Task<T> DeleteProductAsync<T>(int id);
}
