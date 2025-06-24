using App.Data.Data.Entities;
using App.DTO.DTOs;
using AutoMapper;
using e_TicaretApp.Mvc.Models.ViewModels;

namespace e_TicaretApp.Mvc.Mapping
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<CartItemDTO, CartItemViewModel>().ReverseMap();
            CreateMap<ShopDTO, ShopViewModel>().ReverseMap();
            CreateMap<RegisterViewModel, RegisterRequestDTO>().ReverseMap();
            CreateMap<ChangePasswordDTO, ChangePasswordViewModel>().ReverseMap();
            CreateMap<ProductDetailDTO, ProductDetailViewModel>();
            CreateMap<ProductCommentDTO, ProductCommentViewModel>();
            CreateMap<ProductUpdateDTO, ProductUpdateViewModel>().ReverseMap();
            CreateMap<ProductUpdateViewModel, ProductUpdateDTO>();
            CreateMap<ProductUpdateViewModel, ProductUpdateDTO>().ReverseMap()
                .ForMember(dest => dest.CurrentImageUrl, opt => opt.Ignore());
            CreateMap<SaveProductCommentDTO, SaveProductCommentViewModel>().ReverseMap();
            CreateMap<OrderCreateViewModel,OrderDTO>().ReverseMap();
            CreateMap<CartItemViewModel, OrderCreateViewModel>().ReverseMap();
            CreateMap<CartItemDTO, OrderCreateViewModel>().ReverseMap();
            CreateMap<ProductTableItemDTO, ProductTableItem>().ReverseMap();
            CreateMap<UserViewModel, ExistingUserDTO>().ReverseMap();
            CreateMap<ExistingUserDTO,EditViewModel>().ReverseMap();
            CreateMap<ProductEntity, ProductViewDTO>()
                 .ForMember(dest => dest.CategoryName, opt => opt.MapFrom(src => src.Category.Name))
                 .ForMember(dest => dest.ImageUrl, opt => opt.MapFrom(src =>
                     src.Images.FirstOrDefault().Url ?? "/assets/img/default-product.png"));
            CreateMap<ProductViewDTO, MyProductsViewModel>().ReverseMap();
            CreateMap<LoginViewModel, LoginRequestDTO>().ReverseMap();
            CreateMap<ProductDTO, ProductViewModel>().ReverseMap();
            CreateMap<ProductImageDTO, ImageViewModel>().ReverseMap();
            CreateMap<CategoryDTO, CategoryViewModel>().ReverseMap();
        }
    }
}
