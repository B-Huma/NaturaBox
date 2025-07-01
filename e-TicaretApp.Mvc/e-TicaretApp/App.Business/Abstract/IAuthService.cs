using App.DTO.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Business.Abstract
{
    public interface IAuthService
    {
        Task<LoginResponseDTO?> LoginRequestAsync(LoginRequestDTO dto);
        Task RegisterAsync(RegisterRequestDTO dto);
        Task ChangePassword(ChangePasswordDTO dto);
        Task Logout();

    }
}
