using AutoMapper;
using FlowardApp.Services.CatalogService.Dtos;
using FlowardApp.Services.CatalogService.Models;

namespace FlowardApp.Services.CatalogService.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Product, ProductDto>().ReverseMap();
            CreateMap<Product, ProductCreateDto>().ReverseMap();
            CreateMap<Product, ProductUpdateDto>().ReverseMap();
        }
    }
}
