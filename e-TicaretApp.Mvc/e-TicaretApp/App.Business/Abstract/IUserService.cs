using App.DTO.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Business.Abstract
{
    public interface IUserService
    {
        Task<List<AdminUserDTO>> GetNonAdminUsers();
        Task<List<AdminUserDTO>> GetUnapprovedUsers();
        Task ApproveUser(int id);
        Task DeleteRequest(int id);
    }
}
