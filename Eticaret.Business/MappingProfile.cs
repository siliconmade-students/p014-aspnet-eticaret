using AutoMapper;
using Eticaret.Business.Dtos;
using Eticaret.Data.Entity;

namespace Eticaret.Business
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Brand, BrandDto>().ReverseMap();
            CreateMap<User, UserDto>().ReverseMap();
            CreateMap<Product, ProductDto>().ReverseMap();
            CreateMap<Category, CategoryDto>().ReverseMap();
            CreateMap<UserAddress, UserAddressDto>().ReverseMap();
        }
    }
}
