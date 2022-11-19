using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace Shop.Services.CouponAPI;

public class CouponRepository : ICouponRepository
{
    private readonly ApplicationDbContext db;
    private IMapper mapper;

    public CouponRepository(ApplicationDbContext db, IMapper mapper)
    {
        this.db = db;
        this.mapper = mapper;
    }

    public async Task<CouponDTO> GetCouponByCode(string couponCode)
    {
        var coupon = await db.Coupons.FirstOrDefaultAsync(x => x.CouponCode == couponCode);

        return mapper.Map<CouponDTO>(coupon);
    }
}
