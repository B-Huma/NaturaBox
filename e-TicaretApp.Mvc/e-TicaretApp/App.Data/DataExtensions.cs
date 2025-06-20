using App.Data.Data;
using App.Data.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Data
{
    public static class DataExtensions
    {
        public static void AddData(this IServiceCollection services, string connectionString)
        {
            services.AddDbContext<DbContext, AppDbContext>(options =>
            {
                options.UseSqlServer(connectionString);
            });

            //services.AddScoped(typeof(IDataRepository<>), typeof(DataRepository<>));
            //services.AddScoped<IUserRepository, UserRepository>();
            //services.AddScoped<IAdminCategoryRepository, AdminCategoryRepository>();
            //services.AddScoped<IAdminCommentRepository, AdminCommentRepository>();
            //services.AddScoped<IAdminUserRepository, AdminUserRepository>();
            //services.AddScoped<ICartRepository, CartRepository>();
            //services.AddScoped<IOrderRepository, OrderRepository>();
            //services.AddScoped<IProductRepository, ProductRepository>();
        }
    }
}
