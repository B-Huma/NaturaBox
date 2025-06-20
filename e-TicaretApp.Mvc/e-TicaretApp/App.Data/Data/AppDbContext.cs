using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using App.Data.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace App.Data.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }
        public DbSet<UserEntity> Users { get; set; }
        public DbSet<RoleEntity> Roles { get; set; }
        public DbSet<ProductEntity> Products { get; set; }
        public DbSet<ProductImageEntity> ProductImages { get; set; }
        public DbSet<ProductCommentEntity> ProductComments { get; set; }
        public DbSet<CategoryEntity> Categories { get; set; }
        public DbSet<OrderEntity> Orders { get; set; }
        public DbSet<CartItemEntity> CartItems { get; set; }       
        
        public DbSet<OrderItemEntity> OrderItems { get; set; }
        

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<RoleEntity>().HasData(
                new RoleEntity { Id = 1, Name = "admin", CreatedAt = DateTime.Now },
                new RoleEntity { Id = 2, Name = "seller", CreatedAt = DateTime.Now },
                new RoleEntity { Id = 3, Name = "buyer", CreatedAt = DateTime.Now }
            );
            modelBuilder.Entity<UserEntity>().HasData(
                new UserEntity()
                {
                    Id = 1,
                    Email = "admin@admin.com",
                    FirstName = "Betül Hüma",
                    LastName = "Özdemir",
                    Password = "112233",
                    RoleId = 1,
                    Enabled = true,
                    CreatedAt = DateTime.Now,
                },
                new UserEntity
                {
                    Id = 2,
                    Email = "fatih@gmail.com",
                    FirstName = "Fatih",
                    LastName = "Özdemir",
                    Password = "123456",
                    RoleId = 2,
                    Enabled = true,
                    CreatedAt = DateTime.Now
                }
            );
            modelBuilder.Entity<CategoryEntity>().HasData(
                new CategoryEntity { Id = 1, Name = "Organic Vegetables", Color = "#7BB661", IconCssClass = "fa-solid fa-carrot", CreatedAt = DateTime.Now },
                new CategoryEntity { Id = 2, Name = "Organic Fruits", Color = "#FF6347", IconCssClass = "fa-solid fa-apple-alt", CreatedAt = DateTime.Now },
                new CategoryEntity { Id = 3, Name = "Organic Nuts", Color = "#D2B48C", IconCssClass = "fa-solid fa-seedling", CreatedAt = DateTime.Now },
                new CategoryEntity { Id = 4, Name = "Dairy Products", Color = "#F8E473", IconCssClass = "fa-solid fa-cheese", CreatedAt = DateTime.Now },
                new CategoryEntity { Id = 5, Name = "Organic Meat & Chicken", Color = "#E9967A", IconCssClass = "fa-solid fa-drumstick-bite", CreatedAt = DateTime.Now },
                new CategoryEntity { Id = 6, Name = "Natural Legumes", Color = "#B8860B", IconCssClass = "fa-solid fa-pepper-hot", CreatedAt = DateTime.Now },
                new CategoryEntity { Id = 7, Name = "Breakfast Items", Color = "#FFE4B5", IconCssClass = "fa-solid fa-bread-slice", CreatedAt = DateTime.Now },
                new CategoryEntity { Id = 8, Name = "Vegetable Oils", Color = "#DAA520", IconCssClass = "fa-solid fa-oil-can", CreatedAt = DateTime.Now },
                new CategoryEntity { Id = 9, Name = "Natural Cosmetics", Color = "#90EE90", IconCssClass = "fa-solid fa-leaf", CreatedAt = DateTime.Now },
                new CategoryEntity { Id = 10, Name = "Organic Baby Products", Color = "#FFDAB9", IconCssClass = "fa-solid fa-baby", CreatedAt = DateTime.Now }
            );

            modelBuilder.Entity<ProductEntity>().HasData(
                new ProductEntity
                {
                    Id = 1,
                    SellerId = 1,
                    CategoryId = 2,
                    Name = "Strawberry",
                    Price = 85,
                    Details = "Fresh and juicy strawberries.",
                    StockAmount = 50,
                    CreatedAt = DateTime.Now,
                    Enabled = true,
                },
                new ProductEntity
                {
                    Id = 2,
                    SellerId = 2,
                    CategoryId = 2,
                    Name = "Lemon",
                    Price = 35,
                    Details = "Sour and healthy lemons.",
                    StockAmount = 50,
                    CreatedAt = DateTime.Now,
                    Enabled = true
                },
                new ProductEntity
                {
                    Id = 3,
                    SellerId = 2,
                    CategoryId = 2,
                    Name = "Kiwi",
                    Price = 65,
                    Details = "High in vitamin C, help maintain digestive system.",
                    StockAmount = 50,
                    CreatedAt = DateTime.Now,
                    Enabled = true
                }
            );

            modelBuilder.Entity<ProductImageEntity>().HasData(
                new ProductImageEntity
                {
                    Id = 1,
                    ProductId = 1,
                    Url = "/assets/img/products/product-img-1.jpg",
                    CreatedAt = DateTime.Now
                },
                new ProductImageEntity
                {
                    Id = 2,
                    ProductId = 2,
                    Url = "/assets/img/products/product-img-3.jpg",
                    CreatedAt = DateTime.Now
                },
                new ProductImageEntity
                {
                    Id = 3,
                    ProductId = 3,
                    Url = "/assets/img/products/product-img-4.jpg",
                    CreatedAt = DateTime.Now
                }
            );
            modelBuilder.ApplyConfiguration(new UserEntityConfiguration());
            modelBuilder.ApplyConfiguration(new RoleEntityConfiguration());
            modelBuilder.ApplyConfiguration(new ProductEntityConfiguration());
            modelBuilder.ApplyConfiguration(new ProductImageEntityConfiguration());
            modelBuilder.ApplyConfiguration(new ProductCommentEntityConfiguration());
            modelBuilder.ApplyConfiguration(new CategoryEntityConfiguration());
            modelBuilder.ApplyConfiguration(new CartItemEntityConfiguration());
            modelBuilder.ApplyConfiguration(new OrderEntityConfiguration());
            modelBuilder.ApplyConfiguration(new OrderItemEntityConfiguration());

        }
    }
}
