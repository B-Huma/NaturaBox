using App.Data.Data.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Data.Repositories
{
    public interface IProductRepository : IDataRepository<ProductEntity>
    {
        Task<ProductEntity?> GetById(int id);
        Task<IEnumerable<ProductEntity>> GetAllWithCategoryAndSeller();
        Task AddProduct(ProductEntity product);
        Task<ProductEntity> GetProduct(int id);
        Task DeleteProduct(int id);
        Task<bool> HasProduct(int id);
        Task<ProductEntity> GetProductWithDetailsAsync(int id);
        Task<IEnumerable<ProductEntity>> GetSellersProducts(int userId);
    }
    public class ProductRepository : DataRepository<ProductEntity>, IProductRepository
    {
        public ProductRepository(DbContext context) : base(context)
        {

        }

        public async Task AddProduct(ProductEntity product)
        {
            await _dbSet.AddAsync(product);
        }

        public async Task DeleteProduct(int id)
        {
            var product = await GetProduct(id);
            await DeleteAsync(product);
        }

        public async Task<IEnumerable<ProductEntity>> GetAllWithCategoryAndSeller()
        {
            return await _dbSet
                .Include(p=>p.Images)
                .Include(p=>p.Category)
                .Include(p=>p.Seller)
                .ToListAsync();
        }

        public async Task<ProductEntity?> GetById(int id)
        {
            return await GetByIdAsync(id);
        }

        public async Task<ProductEntity?> GetProduct(int id)
        {
            return await _dbSet
                .Include(p => p.Images)
                .Include(p=>p.Category)
                .FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<ProductEntity> GetProductWithDetailsAsync(int id)
        {
            return await _dbSet
                .Include(p => p.Images)
                .Include(p => p.Category)
                .Include(p => p.Comments)
                .ThenInclude(c => c.User)
                .FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task<IEnumerable<ProductEntity>> GetSellersProducts(int userId)
        {
            return await _dbSet
                .Where(x => x.SellerId == userId)
                .Include(p => p.Images)
                .Include(x => x.Category)
                .ToListAsync();
        }

        public async Task<bool> HasProduct(int id)
        {
            await _dbSet.AnyAsync(x => x.Id == id);
            return true;
        }

    }
}
