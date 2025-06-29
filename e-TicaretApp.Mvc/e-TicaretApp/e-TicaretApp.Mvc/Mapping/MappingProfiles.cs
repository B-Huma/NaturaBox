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
            CreateMap<ProductDetailDTO, ProductDetailViewModel>().ReverseMap();
            CreateMap<ProductDetailDTO, ProductUpdateViewModel>();
            CreateMap<ProductCommentDTO, ProductCommentViewModel>();
            CreateMap<ProductUpdateDTO, ProductUpdateViewModel>().ReverseMap();
            CreateMap<ProductUpdateViewModel, ProductUpdateDTO>();
            CreateMap<ProductUpdateViewModel, ProductUpdateDTO>().ReverseMap()
                .ForMember(dest => dest.ImageUrl, opt => opt.Ignore());
            CreateMap<SaveProductCommentDTO, SaveProductCommentViewModel>().ReverseMap();
            CreateMap<OrderCreateViewModel,OrderDTO>().ReverseMap();
            CreateMap<CartItemViewModel, OrderCreateViewModel>().ReverseMap();
            CreateMap<CartItemDTO, OrderCreateViewModel>().ReverseMap();
            CreateMap<ProductTableItemDTO, ProductTableItem>().ReverseMap();
            CreateMap<UserViewModel, ExistingUserDTO>().ReverseMap();
            CreateMap<ExistingUserDTO,EditViewModel>().ReverseMap();            
            CreateMap<ProductViewDTO, MyProductsViewModel>().ReverseMap();
            CreateMap<LoginViewModel, LoginRequestDTO>().ReverseMap();
            CreateMap<ProductDTO, ProductViewModel>().ReverseMap();
            CreateMap<ProductImageDTO, ImageViewModel>().ReverseMap();
            CreateMap<CategoryDTO, CategoryViewModel>().ReverseMap();
            CreateMap<UserEditDTO, EditViewModel>().ReverseMap();
            CreateMap<ProductViewDTO, ProductTableItem>().ReverseMap();
            CreateMap<ProductCreateViewModel, ProductCreateDTO>().ReverseMap();
        }
    }
}
