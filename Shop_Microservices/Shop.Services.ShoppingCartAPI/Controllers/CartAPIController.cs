using Microsoft.AspNetCore.Mvc;

namespace Shop.Services.ShoppingCartAPI;


[Route("api/cart")]
public class CartAPIController : ControllerBase
{
    private readonly ICartRepository cartRepository;
    protected ResponseDTO response { get; set; }

    public CartAPIController(ICartRepository cartRepository)
    {
        this.cartRepository = cartRepository;
        response = new ResponseDTO();
    }

    [HttpGet]
    [Route("GetCart/{id}")]
    public async Task<ResponseDTO> GetCart(int id)
    {
        try
        {
            var cartDTO = await cartRepository.GetCartById(id);
            response.Result = cartDTO;
        }
        catch (Exception ex)
        {
            response.IsSuccess = false;
            response.ErrorMessages = new List<string>() { ex.Message };
        }

        return response;
    }

    [HttpPost]
    [Route("AddCart")]
    public async Task<ResponseDTO> AddCart([FromBody] CartDTO cartDTO)
    {
        try
        {
            var cart = await cartRepository.CreateUpdateCart(cartDTO);
            response.Result = cart;
        }
        catch (Exception ex)
        {
            response.IsSuccess = false;
            response.ErrorMessages = new List<string>() { ex.Message };
        }

        return response;
    }

    [HttpPost]
    [Route("UpdateCart")]
    public async Task<ResponseDTO> UpdateCart([FromBody] CartDTO cartDTO)
    {
        try
        {
            var cart = await cartRepository.CreateUpdateCart(cartDTO);
            response.Result = cart;
        }
        catch (Exception ex)
        {
            response.IsSuccess = false;
            response.ErrorMessages = new List<string>() { ex.Message };
        }

        return response;
    }

    [HttpPost]
    [Route("RemoveCart")]
    public async Task<ResponseDTO> RemoveCart([FromBody] int cartId)
    {
        try
        {
            var isSuccess = await cartRepository.RemoveFromCart(cartId);
            response.Result = isSuccess;
        }
        catch (Exception ex)
        {
            response.IsSuccess = false;
            response.ErrorMessages = new List<string>() { ex.Message };
        }

        return response;
    }

    [HttpPost]
    [Route("ApplyCoupon")]
    public async Task<ResponseDTO> ApplyCoupon([FromBody] CartDTO cartDTO)
    {
        try
        {
            var isSuccess = await cartRepository.ApplyCoupon(cartDTO.CartHeader.Id, cartDTO.CartHeader.CouponCode);
            response.Result = isSuccess;
        }
        catch (Exception ex)
        {
            response.IsSuccess = false;
            response.ErrorMessages = new List<string>() { ex.Message };
        }

        return response;
    }

    [HttpPost]
    [Route("RemoveCoupon")]
    public async Task<ResponseDTO> RemoveCoupon([FromBody] int id)
    {
        try
        {
            var isSuccess = await cartRepository.RemoveCoupon(id);
            response.Result = isSuccess;
        }
        catch (Exception ex)
        {
            response.IsSuccess = false;
            response.ErrorMessages = new List<string>() { ex.Message };
        }

        return response;
    }

    [HttpPost]
    [Route("Checkout")]
    public async Task<object> Checkout([FromBody] CheckoutHeaderDTO checkoutHeaderDTO)
    {
        try
        {
            var cartDto = await cartRepository.GetCartById(4);

            if (cartDto == null)
                return BadRequest();

            checkoutHeaderDTO.CartDetails = cartDto.CartDetails;
            checkoutHeaderDTO.CartTotalItems = checkoutHeaderDTO.CartDetails.Count();
            response.Result = checkoutHeaderDTO;

        }
        catch (Exception ex)
        {
            response.IsSuccess = false;
            response.ErrorMessages = new List<string>() { ex.Message };
        }

        return response;
    }
}

