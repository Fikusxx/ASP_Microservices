using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Shop.Web;

public class CartController : Controller
{
    private readonly IProductService productService;
    private readonly ICartService cartService;
    private readonly ICouponService couponService;

    public CartController(IProductService productService, ICartService cartService,
        ICouponService couponService)
    {
        this.productService = productService;
        this.cartService = cartService;
        this.couponService = couponService;
    }

    [HttpGet]
    public async Task<IActionResult> CartIndex()
    {
        var cartDTO = await LoadCart();
        return View(cartDTO);
    }

    [HttpPost]
    public async Task<IActionResult> ApplyCoupon(CartDTO cartDTO)
    {
        var response = await cartService.ApplyCoupon<ResponseDTO>(cartDTO);

        if (response != null && response.IsSuccess)
        {
            return RedirectToAction(nameof(CartIndex));
        }

        return View();
    }

    [HttpPost]
    public async Task<IActionResult> RemoveCoupon(CartDTO cartDTO)
    {
        var response = await cartService.RemoveCoupon<ResponseDTO>(cartDTO.CartHeader.Id);

        if (response != null && response.IsSuccess)
        {
            return RedirectToAction(nameof(CartIndex));
        }

        return View();
    }

    [HttpGet]
    public async Task<IActionResult> Remove(int id)
    {
        var response = await cartService.RemoveFromCartAsync<ResponseDTO>(id);

        if (response != null && response.IsSuccess)
        {
            return RedirectToAction(nameof(CartIndex));
        }

        return View();
    }

    [HttpGet]
    public async Task<IActionResult> Checkout()
    {
        var cartDTO = await LoadCart();
        return View(cartDTO);
    }

    [HttpPost]
    public async Task<IActionResult> Checkout(CartDTO cartDTO)
    {
        try
        {
            var response = await cartService.Checkout<ResponseDTO>(cartDTO.CartHeader);
            // add order using OrderService (OrderAPI)
            return RedirectToAction(nameof(Confirmation));
        }
        catch (Exception ex)
        {
            return View(cartDTO);
        }
    }

    [HttpGet]
    public async Task<IActionResult> Confirmation()
    {
        return View();
    }

    private async Task<CartDTO> LoadCart()
    {
        var response = await cartService.GetCartByIdAsync<ResponseDTO>(4);

        var cartDTO = new CartDTO();

        if (response != null && response.IsSuccess)
        {
            cartDTO = JsonConvert.DeserializeObject<CartDTO>(Convert.ToString(response.Result));
        }

        if (cartDTO.CartHeader != null)
        {
            var couponCode = cartDTO.CartHeader.CouponCode;
            CouponDTO? coupon = null;

            if (string.IsNullOrEmpty(couponCode) == false)
            {
                var resp = await couponService.GetCoupon<ResponseDTO>(couponCode);

                if (resp != null && resp.IsSuccess)
                {
                    coupon = JsonConvert.DeserializeObject<CouponDTO>(Convert.ToString(resp.Result));
                    cartDTO.CartHeader.DiscountTotal = coupon.DiscountAmount;
                }
            }

            foreach (var detail in cartDTO.CartDetails)
            {
                cartDTO.CartHeader.OrderTotal += (detail.Product.Price * detail.Count);
            }

            cartDTO.CartHeader.OrderTotal -= cartDTO.CartHeader.DiscountTotal;
        }

        return cartDTO;
    }
}
