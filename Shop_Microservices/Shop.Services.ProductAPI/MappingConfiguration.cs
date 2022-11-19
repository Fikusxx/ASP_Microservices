using AutoMapper;

namespace Shop.Services.ProductAPI;

public class MappingConfiguration
{
    public static MapperConfiguration RegisterMaps()
    {
        var mappingConfiguration = new MapperConfiguration(config => 
        {
            config.CreateMap<ProductDTO, Product>();
            config.CreateMap<Product, ProductDTO>();
        });

        return mappingConfiguration;
    }
}
