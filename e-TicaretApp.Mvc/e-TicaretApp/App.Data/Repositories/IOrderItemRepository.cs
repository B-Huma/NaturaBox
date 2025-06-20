using App.Data.Data.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Data.Repositories
{
    public interface IOrderItemRepository : IDataRepository<OrderItemEntity>
    {
        Task CreateOrderItem(IEnumerable<OrderItemEntity> orderItem);
    }
    public class OrderItemRepository : DataRepository<OrderItemEntity>, IOrderItemRepository
    {
        public OrderItemRepository(DbContext context) : base(context)
        {
        }

        public async Task CreateOrderItem(IEnumerable<OrderItemEntity> orderItem)
        {
            await AddRangeAsync(orderItem);
        }
    }
}
