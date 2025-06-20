using App.Data.Data.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Data.Repositories
{
    public interface IAdminUserRepository : IDataRepository<UserEntity>
    {
        Task<List<UserEntity>> GetUsersWithoutAdmin(); 
        Task<List<UserEntity>> UnApprovedUsers();
    }
    public class AdminUserRepository : DataRepository<UserEntity>, IAdminUserRepository
    {
        public AdminUserRepository(DbContext context) : base(context) 
        {
            
        }
        public async Task<List<UserEntity>> GetUsersWithoutAdmin()
        {
            return await _dbSet
                .Include(x=>x.Role)
                .Where(x=>x.Role.Name != "admin")
                .ToListAsync();
        }

        public async Task<List<UserEntity>> UnApprovedUsers()
        {
            return await _dbSet
                .Include(x=>x.Role)
                .Where(x=> x.Role.Name != "admin")
                .Where(x=>!x.Enabled)
                .ToListAsync();
        }
    }
}
