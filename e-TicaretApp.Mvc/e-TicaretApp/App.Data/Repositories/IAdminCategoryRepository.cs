using App.Data.Data.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Data.Repositories
{
    public interface IAdminCategoryRepository : IDataRepository<CategoryEntity>
    {
        Task<IEnumerable<CategoryEntity>> GetCategoriesAsync();
        Task AddCategoryAsync(CategoryEntity category);
        Task EditCategoryAsync(int id, CategoryEntity category);
        Task DeleteCategoryAsync(int id);
        Task<bool> CategoryExists(int id);
    }
    public class AdminCategoryRepository : DataRepository<CategoryEntity>, IAdminCategoryRepository
    {
        public AdminCategoryRepository(DbContext context) : base(context)
        {
            
        }
        public async Task AddCategoryAsync(CategoryEntity category)
        {
            await AddAsync(category);
        }

        public async Task<bool> CategoryExists(int id)
        {
            await _dbSet.FirstOrDefaultAsync(x => x.Id == id);
            return true;
        }

        public async Task DeleteCategoryAsync(int id)
        {
            var category = await _dbSet.FindAsync(id);
            if (category != null)
            {
                await DeleteAsync(category);
            }
        }

        public async Task EditCategoryAsync(int id, CategoryEntity category)
        {
            var existingCategory = await GetByIdAsync(id);
            if (existingCategory != null)
            {
                existingCategory.Name = category.Name;
                existingCategory.Color = category.Color;
                existingCategory.IconCssClass = category.IconCssClass;

                await UpdateAsync(existingCategory);
                
            }
        }


        public async Task<IEnumerable<CategoryEntity>> GetCategoriesAsync()
        {
            return await GetListAsync();
        }
    }
}
