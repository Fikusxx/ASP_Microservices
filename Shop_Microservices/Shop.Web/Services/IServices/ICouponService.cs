namespace Shop.Web;

public interface ICouponService
{
    public Task<T> GetCoupon<T>(string code);
}
