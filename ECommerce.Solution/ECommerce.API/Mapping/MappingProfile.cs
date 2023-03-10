using AutoMapper;
using ECommerce.API.DTO;
using ECommerce.API.Helpers;
using ECommerce.Core.Core.Models.Entities;

namespace ECommerce.API.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Product,ProductDto>()
                .ForMember(dest => dest.ProductBrand, o=> o.MapFrom(src=> src.ProductBrand.Name))
                .ForMember(dest => dest.ProductType, o => o.MapFrom(src => src.ProductType.Name))
                .ForMember(dest => dest.PictureUrl, o=> o.MapFrom<ProductUrlResolver>())
                .ReverseMap();
        }
    }
}
