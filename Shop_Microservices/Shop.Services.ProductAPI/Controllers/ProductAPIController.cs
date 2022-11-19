using Microsoft.AspNetCore.Mvc;

namespace Shop.Services.ProductAPI;

[Route("api/products")]
public class ProductAPIController : ControllerBase
{
    protected ResponseDTO response { get; set; }
    private readonly IProductRepository productRepository;

    public ProductAPIController(IProductRepository productRepository)
    {
        this.productRepository = productRepository;
        response = new ResponseDTO();
    }

    [HttpGet]
    public async Task<ResponseDTO> Get()
    {
        try
        {
            var productDTOList = await productRepository.GetProductsAsync();
            response.Result = productDTOList;
        }
        catch (Exception ex)
        {
            response.IsSuccess = false;
            response.ErrorMessages = new List<string>() { ex.Message };
        }

        return response;
    }

    [HttpGet]
    [Route("{id}")]
    public async Task<ResponseDTO> Get(int id)
    {
        try
        {
            var productDTO = await productRepository.GetProductByIdAsync(id);
            response.Result = productDTO;
        }
        catch (Exception ex)
        {
            response.IsSuccess = false;
            response.ErrorMessages = new List<string>() { ex.Message };
        }

        return response;
    }

    [HttpPost]
    public async Task<ResponseDTO> Post([FromBody] ProductDTO productDTO)
    {
        try
        {
            var model = await productRepository.CreateProductAsync(productDTO);
            response.Result = model;
        }
        catch (Exception ex)
        {
            response.IsSuccess = false;
            response.ErrorMessages = new List<string>() { ex.Message };
        }

        return response;
    }

    [HttpPut]
    public async Task<ResponseDTO> Put([FromBody] ProductDTO productDTO)
    {
        try
        {
            var model = await productRepository.UpdateProductAsync(productDTO);
            response.Result = model;
        }
        catch (Exception ex)
        {
            response.IsSuccess = false;
            response.ErrorMessages = new List<string>() { ex.Message };
        }

        return response;
    }

    [HttpDelete]
    [Route("{id}")]
    public async Task<ResponseDTO> Delete(int id)
    {
        try
        {
            var isSuccess = await productRepository.DeleteProductAsync(id);
            response.Result = isSuccess;
        }
        catch (Exception ex)
        {
            response.IsSuccess = false;
            response.ErrorMessages = new List<string>() { ex.Message };
        }

        return response;
    }
}
