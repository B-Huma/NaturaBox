using AdminMvc.Models.ViewModels;
using App.DTO.DTOs;
using AutoMapper;

namespace AdminMvc.Mapping
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<CategoryDTO, CategoryViewModel>().ReverseMap();

        }
    }
}
