using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace Shop.Services.ProductAPI;

public class ProductRepository : IProductRepository
{
    private readonly ApplicationDbContext db;
    private IMapper mapper;

    public ProductRepository(ApplicationDbContext db, IMapper mapper)
    {
        this.db = db;
        this.mapper = mapper;
    }

    public async Task<ProductDTO> CreateProductAsync(ProductDTO productDTO)
    {
        var product = mapper.Map<ProductDTO, Product>(productDTO);
        await db.Products.AddAsync(product);
        await db.SaveChangesAsync();

        return mapper.Map<Product, ProductDTO>(product);
    }

    public async Task<bool> DeleteProductAsync(int id)
    {
        var product = await db.Products.FirstOrDefaultAsync(x => x.Id == id);

        if (product == null)
            return false;

        db.Products.Remove(product);
        await db.SaveChangesAsync();

        return true;
    }

    public async Task<ProductDTO> GetProductByIdAsync(int id)
    {
        var product = await db.Products.FirstOrDefaultAsync(x => x.Id == id);
        var productDTO = mapper.Map<ProductDTO>(product);

        return productDTO;
    }

    public async Task<IEnumerable<ProductDTO>> GetProductsAsync()
    {
        var productList = await db.Products.ToListAsync();
        var productDTOList = mapper.Map<List<ProductDTO>>(productList);

        return productDTOList;
    }

    public async Task<ProductDTO> UpdateProductAsync(ProductDTO productDTO)
    {
        var product = mapper.Map<ProductDTO, Product>(productDTO);
        db.Products.Update(product);
        await db.SaveChangesAsync();

        return mapper.Map<Product, ProductDTO>(product);
    }
}
