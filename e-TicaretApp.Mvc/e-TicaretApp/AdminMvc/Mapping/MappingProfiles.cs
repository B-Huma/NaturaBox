using AdminMvc.Models.ViewModels;
using App.Data.Data.Entities;
using App.DTO.DTOs;
using AutoMapper;

namespace AdminMvc.Mapping
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<CategoryDTO, CategoryViewModel>().ReverseMap();
            CreateMap<LoginViewModel, LoginRequestDTO>().ReverseMap();
            CreateMap<UserEntity, AdminUserDTO>()
                .ForMember(dest => dest.Role, opt => opt.MapFrom(src => src.Role.Name));
        }
    }
}
