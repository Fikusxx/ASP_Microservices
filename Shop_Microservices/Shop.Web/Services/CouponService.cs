namespace Shop.Web;

public class CouponService : BaseService, ICouponService
{
    public CouponService(IHttpClientFactory HttpClientFactory) : base(HttpClientFactory)
    { }

    public async Task<T> GetCoupon<T>(string code)
    {
        var apiRequest = new ApiRequest()
        {
            ApiType = StaticDetails.ApiType.GET,
            Url = StaticDetails.CouponAPIBase + "api/coupon/" + code,
            AccessToken = ""
        };

        return await SendAsync<T>(apiRequest);
    }
}
