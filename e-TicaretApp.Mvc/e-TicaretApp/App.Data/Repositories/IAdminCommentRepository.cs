using App.Data.Data.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace App.Data.Repositories
{
    public interface IAdminCommentRepository : IDataRepository<ProductCommentEntity>
    {
        Task<List<ProductCommentEntity>> GetAllComments();
        Task<List<ProductCommentEntity>> GetAllUnapprovedComments();
        Task ApproveComment(int id);
        Task DeleteComment(int id);
        Task<bool> HasProductComment (int id, int userId);
        Task AddComment(ProductCommentEntity entity);
    }
    public class AdminCommentRepository : DataRepository<ProductCommentEntity>, IAdminCommentRepository
    {
        public AdminCommentRepository(DbContext context) : base(context)
        {
        }

        public async Task<List<ProductCommentEntity>> GetAllComments()
        {
            return await _dbSet
                .Include(x=>x.Product)
                .Include(x=>x.User)
                .ToListAsync();
        }

        public async Task<List<ProductCommentEntity>> GetAllUnapprovedComments()
        {
            return await _dbSet
                .Include(x=>x.Product)
                .Include(x=>x.User)
                .Where(x=>!x.IsConfirmed)
                .ToListAsync();
        }
        public async Task ApproveComment(int id)
        {
            var comment = await GetByIdAsync(id);
            if (comment != null)
            {
                comment.IsConfirmed = true;
                UpdateAsync(comment);
                
            }

        }

        public async Task DeleteComment(int id)
        {
            var comment = await GetByIdAsync(id);
            await DeleteAsync(comment);
            
        }

        public async Task<bool> HasProductComment(int id, int userId)
        {
            await _dbSet.AnyAsync(x=>x.ProductId == id && x.UserId == userId);
            return true;
        }

        public async Task AddComment(ProductCommentEntity entity)
        {
            await _dbSet.AddAsync(entity);
        }
        
    }

}
