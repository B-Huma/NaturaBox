using App.DTO.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Business.Abstract
{
    public interface ICategoryService
    {
        Task<List<CategoryDTO>> GetCategories();
        Task<CategoryDTO> GetCategoryById(int id);
        Task CreateCategory(CategoryDTO dto);
        Task EditCategory(int id, CategoryDTO dto);
        Task DeleteCategory(int id);
    }
}
