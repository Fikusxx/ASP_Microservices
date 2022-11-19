using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Shop.Web;

public class ProductController : Controller
{
    private readonly IProductService productService;

    public ProductController(IProductService productService)
    {
        this.productService = productService;
    }

    [HttpGet]
    public async Task<IActionResult> ProductIndex()
    {
        var list = new List<ProductDTO>();
        var response = await productService.GetAllProductsAsync<ResponseDTO>();

        if (response != null && response.IsSuccess)
        {
            list = JsonConvert.DeserializeObject<List<ProductDTO>>(Convert.ToString(response.Result));
        }

        return View(list);
    }

    [HttpGet]
    public async Task<IActionResult> ProductCreate()
    {
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> ProductCreate(ProductDTO model)
    {
        if (ModelState.IsValid)
        {
            var response = await productService.CreateProductAsync<ResponseDTO>(model);

            if (response != null && response.IsSuccess)
            {
                return RedirectToAction(nameof(ProductIndex));
            }
        }

        return View(model);
    }

    [HttpGet]
    public async Task<IActionResult> ProductEdit(int id)
    {
        var response = await productService.GetProductByIdAsync<ResponseDTO>(id);

        if (response != null && response.IsSuccess)
        {
            var model = JsonConvert.DeserializeObject<ProductDTO>(Convert.ToString(response.Result));

            return View(model);
        }

        return NotFound();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> ProductEdit(ProductDTO model)
    {
        if (ModelState.IsValid)
        {
            var response = await productService.UpdateProductAsync<ResponseDTO>(model);

            if (response != null && response.IsSuccess)
            {
                return RedirectToAction(nameof(ProductIndex));
            }
        }

        return View(model);
    }

    [HttpGet]
    public async Task<IActionResult> ProductDelete(int id)
    {
        var response = await productService.GetProductByIdAsync<ResponseDTO>(id);

        if (response != null && response.IsSuccess)
        {
            var model = JsonConvert.DeserializeObject<ProductDTO>(Convert.ToString(response.Result));

            return View(model);
        }

        return NotFound();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> ProductDelete(ProductDTO model)
    {
        var response = await productService.DeleteProductAsync<ResponseDTO>(model.Id);

        if (response != null && response.IsSuccess)
        {
            return RedirectToAction(nameof(ProductIndex));
        }

        return RedirectToAction(nameof(ProductIndex));
    }

}
