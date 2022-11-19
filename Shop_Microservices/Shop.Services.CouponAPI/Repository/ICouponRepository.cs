namespace Shop.Services.CouponAPI;

public interface ICouponRepository
{
    public Task<CouponDTO> GetCouponByCode(string couponCode);
}
