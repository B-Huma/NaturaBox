using App.Data.Data.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Data.Repositories
{
    public interface ICartRepository : IDataRepository<CartItemEntity>
    {
        Task<IEnumerable<CartItemEntity>> GetCartDetails(int userId);
        Task<CartItemEntity> AddProductToCart(CartItemEntity entity);
        Task EditCartItemsQuantity(CartItemEntity entity);
        Task DeleteProductFromCart(int productId);
        Task RemoveCartItems(IEnumerable<CartItemEntity> cartItems);
    }
    public class CartRepository : DataRepository<CartItemEntity>, ICartRepository
    {
        public CartRepository(DbContext context) : base(context)
        {
        }
        public async Task<IEnumerable<CartItemEntity>> GetCartDetails(int userId)
        {
            return await _dbSet
                .Include(x=> x.Product)
                .Where(x=>x.UserId == userId)
                .ToListAsync();
        }
        public async Task<CartItemEntity> AddProductToCart(CartItemEntity entity)
        {
            await AddAsync(entity);
            return entity;
        }

        public async Task EditCartItemsQuantity(CartItemEntity entity)
        {
            await UpdateAsync(entity);
        }

        public async Task DeleteProductFromCart(int productId)
        {
            var cartItem = await GetByIdAsync(productId);
            await DeleteAsync(cartItem);
        }

        public async Task RemoveCartItems(IEnumerable<CartItemEntity> cartItems)
        {
            await RemoveRange(cartItems);
        }
    }
}
