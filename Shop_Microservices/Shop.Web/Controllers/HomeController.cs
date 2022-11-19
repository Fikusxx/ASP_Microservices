using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Shop.Web.Models;
using System.Diagnostics;

namespace Shop.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> logger;
        private readonly IProductService productService;
        private readonly ICartService cartService;


        public HomeController(ILogger<HomeController> logger, IProductService productService, 
            ICartService cartService)
        {
            this.logger = logger;
            this.productService = productService;
            this.cartService = cartService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var products = new List<ProductDTO>();
            var response = await productService.GetAllProductsAsync<ResponseDTO>();

            if (response != null && response.IsSuccess)
            {
                products = JsonConvert.DeserializeObject<List<ProductDTO>>(Convert.ToString(response.Result));
            }

            return View(products);
        }

        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            var product = new ProductDTO();
            var response = await productService.GetProductByIdAsync<ResponseDTO>(id);

            if (response != null && response.IsSuccess)
            {
                product = JsonConvert.DeserializeObject<ProductDTO>(Convert.ToString(response.Result));

                if (product == null)
                    return NotFound();
            }

            return View(product);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Details(ProductDTO productDTO)
        {
            var cartDTO = new CartDTO()
            {
                CartHeader = new CartHeaderDTO()
            };

            var cartDetailsDTO = new CartDetailsDTO()
            {
                ProductId = productDTO.Id,
                Count = productDTO.Count
            };

            var response = await productService.GetProductByIdAsync<ResponseDTO>(productDTO.Id);

            if (response != null && response.IsSuccess)
            {
                var product = JsonConvert.DeserializeObject<ProductDTO>(Convert.ToString(response.Result));
                cartDetailsDTO.Product = product;
            }

            var cartDetailsListDTO = new List<CartDetailsDTO>();
            cartDetailsListDTO.Add(cartDetailsDTO);
            cartDTO.CartDetails = cartDetailsListDTO;

            var cartResponse = await cartService.AddToCartAsync<ResponseDTO>(cartDTO);

            if (cartResponse != null && cartResponse.IsSuccess)
            {
                return RedirectToAction(nameof(Index));
            }

            return View(productDTO);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        [HttpGet]
        public IActionResult Login()
        {
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public IActionResult Logout()
        {
            return SignOut("Cookies", "iodc");
        }
    }
}