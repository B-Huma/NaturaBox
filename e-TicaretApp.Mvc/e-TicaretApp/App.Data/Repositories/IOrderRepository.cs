using App.Data.Data.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Data.Repositories
{
    public interface IOrderRepository : IDataRepository<OrderEntity>
    {
        Task<IEnumerable<OrderEntity>> OrderDetailsAsync(int userId);
        Task CreateOrderAsync(OrderEntity order);
    }
    public class OrderRepository : DataRepository<OrderEntity>, IOrderRepository
    {
        public OrderRepository(DbContext context) : base(context)
        {
        }

        public async Task CreateOrderAsync(OrderEntity order)
        {
            await AddAsync(order);
        }

        public async Task<IEnumerable<OrderEntity>> OrderDetailsAsync(int userId)
        {
            return await _dbSet
                .Where(o => o.UserId == userId)
                .Include(x => x.Items)
                .ThenInclude(x => x.Product)
                .OrderByDescending(x => x.CreatedAt)
                .ToListAsync();
        }
    }
}
