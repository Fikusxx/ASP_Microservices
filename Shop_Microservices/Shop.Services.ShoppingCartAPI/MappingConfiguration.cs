using AutoMapper;

namespace Shop.Services.ShoppingCartAPI;

public class MappingConfiguration
{
    public static MapperConfiguration RegisterMaps()
    {
        var mappingConfiguration = new MapperConfiguration(config =>
        {
            config.CreateMap<ProductDTO, Product>();
            config.CreateMap<Product, ProductDTO>();

            config.CreateMap<CartHeaderDTO, CartHeader>();
            config.CreateMap<CartHeader, CartHeaderDTO>();

            config.CreateMap<CartDetailsDTO, CartDetails>();
            config.CreateMap<CartDetails, CartDetailsDTO>();

            config.CreateMap<Cart, CartDTO>();
            config.CreateMap<CartDTO, Cart>();
        });

        return mappingConfiguration;
    }
}
