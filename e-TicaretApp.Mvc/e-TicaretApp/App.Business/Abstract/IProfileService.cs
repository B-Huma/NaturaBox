using App.DTO.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Business.Abstract
{
    public interface IProfileService
    {
        Task<ExistingUserDTO> GetProfileDetails();
        Task<bool> UpdateProfile(int id, UserEditDTO dto);
        Task<List<ProductViewDTO>> GetUserProductsAsync();
        Task<string> RequestSeller();
    }
}
