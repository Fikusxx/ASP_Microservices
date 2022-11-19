namespace Shop.Services.ProductAPI;


public interface IProductRepository
{
    public Task<IEnumerable<ProductDTO>> GetProductsAsync();

    public Task<ProductDTO> GetProductByIdAsync(int id);

    public Task<ProductDTO> CreateProductAsync(ProductDTO productDTO);

    public Task<ProductDTO> UpdateProductAsync(ProductDTO productDTO);

    public Task<bool> DeleteProductAsync(int id);
}

