using App.Data.Data.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Data.Repositories
{
    public interface IUserRepository : IDataRepository<UserEntity>
    {
        Task<UserEntity> GetById(int id);
        Task<UserEntity> GetByEmail(string email);
        Task CreateUser(UserEntity user);
        Task<bool> EmailExists(string email);
        Task UpdateUser(UserEntity user);
        Task<UserEntity> UserIdExists(int userId);
        Task<UserEntity> ExistingUser(int userId);
    }

    public class UserRepository : DataRepository<UserEntity>, IUserRepository
    {
        public UserRepository(DbContext context) : base(context)
        {
        }

        public async Task CreateUser(UserEntity user)
        {
            await AddAsync(user);
        }

        public async Task<bool> EmailExists(string email)
        {
            var user = await _dbSet.FirstOrDefaultAsync(x => x.Email.ToLower() == email.ToLower());
            return user != null;
        }


        public async Task<UserEntity> ExistingUser(int userId)
        {
            return await _dbSet.Include(x => x.Role).FirstOrDefaultAsync(x => x.Id == userId);
        }

        public Task<UserEntity> GetByEmail(string email)
        {
            return _dbSet.FirstOrDefaultAsync(x => x.Email.ToLower() == email.ToLower());
        }


        public async Task<UserEntity> GetById(int id)
        {
            return await GetByIdAsync(id);
        }

        public Task UpdateUser(UserEntity user)
        {
            return UpdateAsync(user);
        }

        public async Task<UserEntity> UserIdExists(int userId)
        {
            return await _dbSet.FirstOrDefaultAsync(x => x.Id == userId);
        }
    }
}
