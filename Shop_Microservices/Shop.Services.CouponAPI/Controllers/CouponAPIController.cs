using Microsoft.AspNetCore.Mvc;

namespace Shop.Services.CouponAPI;

[Route("api/coupon")]
public class CouponAPIController : ControllerBase
{
    private readonly ICouponRepository couponRepository;
    protected ResponseDTO response { get; set; }

    public CouponAPIController(ICouponRepository couponRepository)
    {
        this.couponRepository = couponRepository;
        response = new ResponseDTO();
    }

    [HttpGet]
    [Route("{code}")]
    public async Task<ResponseDTO> Get(string code)
    {
        try
        {
            var couponDTO = await couponRepository.GetCouponByCode(code);
            response.Result = couponDTO;
        }
        catch (Exception ex)
        {
            response.IsSuccess = false;
            response.ErrorMessages = new List<string>() { ex.Message };
        }

        return response;
    }
}

