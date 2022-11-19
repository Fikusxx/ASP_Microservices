using AutoMapper;

namespace Shop.Services.OrderAPI;

public class MappingConfiguration
{
    public static MapperConfiguration RegisterMaps()
    {
        var mappingConfiguration = new MapperConfiguration(config =>
        {
            //config.CreateMap<ProductDTO, Product>();
            //config.CreateMap<ProductDTO, Product>();
        });

        return mappingConfiguration;
    }
}
