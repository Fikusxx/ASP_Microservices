using AutoMapper;

namespace Shop.Services.CouponAPI;

public class MappingConfiguration
{
    public static MapperConfiguration RegisterMaps()
    {
        var mappingConfiguration = new MapperConfiguration(config =>
        {
            config.CreateMap<Coupon, CouponDTO>().ReverseMap();
        });

        return mappingConfiguration;
    }
}
