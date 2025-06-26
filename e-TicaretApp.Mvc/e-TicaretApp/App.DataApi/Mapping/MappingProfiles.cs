using App.Data.Data.Entities;
using App.DTO.DTOs;
using AutoMapper;

namespace App.DataApi.Mapping
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<CategoryEntity, CategoryDTO>().ReverseMap();
            CreateMap<ProductCommentEntity, AdminProductCommentDTO>()
                .ForMember(dest => dest.ProductName, opt => opt.MapFrom(src => src.Product.Name))
                .ForMember(dest => dest.UserFullName, opt => opt.MapFrom(src => src.User.FirstName + " " + src.User.LastName)).ReverseMap();
            CreateMap<ProductEntity, ProductDTO>().ReverseMap();
            CreateMap<CategoryEntity, CategoryDTO>().ReverseMap();
            CreateMap<CartItemEntity, CartItemDTO>()
                .ForMember(dest => dest.ProductName, opt => opt.MapFrom(src => src.Product.Name))
                .ForMember(dest => dest.UnitPrice, opt => opt.MapFrom(src => src.Product.Price))
                .ReverseMap();
            CreateMap<UserEntity, AdminUserDTO>()
                .ForMember(dest => dest.Role, opt => opt.MapFrom(src => src.Role.Name));
            CreateMap<RegisterRequestDTO, UserEntity>()
                .ForMember(dest => dest.RoleId, opt => opt.MapFrom(src => 3));
            CreateMap<ProductCommentEntity, SaveProductCommentDTO>().ReverseMap();
            CreateMap<ProductEntity, ProductDetailDTO>()
                .ForMember(dest => dest.ImageUrl, opt =>
                    opt.MapFrom(src => src.Images.FirstOrDefault().Url ?? "/assets/img/default-product.png"))
                .ForMember(dest => dest.CategoryName, opt =>
                    opt.MapFrom(src => src.Category != null ? src.Category.Name : null))
                .ForMember(dest => dest.Comments, opt =>
                    opt.MapFrom(src => src.Comments.Where(c => !c.IsConfirmed)));
            CreateMap<ProductCommentEntity, ProductCommentDTO>()
                .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.User.FirstName + " " + src.User.LastName));
            CreateMap<UserEntity, ExistingUserDTO>()
                .ForMember(dest => dest.Role, opt=>opt.MapFrom(src=>src.Role.Name));
            CreateMap<UserEntity, UserEditDTO>();
            CreateMap<ProductEntity, ProductTableItemDTO>()
                .ForMember(dest => dest.CategoryName, opt => opt.MapFrom(src => src.Category.Name))
                .ForMember(dest => dest.ImageUrl, opt => opt.MapFrom(src =>
                    src.Images.FirstOrDefault() != null
                        ? src.Images.FirstOrDefault().Url
                        : "/assets/img/default-product.png"));
            CreateMap<UserEntity, UserEditDTO>().ReverseMap();
            CreateMap<ProductEntity, ProductDTO>();
            CreateMap<ProductImageEntity, ProductImageDTO>();
            CreateMap<UserEntity, UserDTO>().ReverseMap();
            CreateMap<CartItemEntity, CartItemDTO>()
                .ForMember(dest => dest.ProductName, opt => opt.MapFrom(src => src.Product.Name))
                .ForMember(dest => dest.UnitPrice, opt => opt.MapFrom(src => src.Product.Price)).ReverseMap();

        }
    }
}
