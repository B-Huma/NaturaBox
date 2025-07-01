using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace App.Data.Data.Entities
{
    public class OrderItemEntity
    {
        // hata çıkarsa jsonignore yaz order'a cycle hatasında
        // siparişteki ürünler
        public int Id { get; set; }
        public int OrderId { get; set; }
        public int ProductId { get; set; }
        [Range(1,int.MaxValue)]
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;

        public OrderEntity Order { get; set; } = null!;
        
        public ProductEntity Product { get; set; } = null!;
        public ICollection<CartItemEntity> CartItems { get; set; } = new List<CartItemEntity>();
    }
    public class OrderItemEntityConfiguration : IEntityTypeConfiguration<OrderItemEntity>
    {
        public void Configure(EntityTypeBuilder<OrderItemEntity> builder)
        {
            builder.HasKey(e => e.Id);

            builder.Property(e => e.OrderId)
                .IsRequired();

            builder.Property(e => e.ProductId)
                .IsRequired();

            builder.HasOne(e => e.Order)
                .WithMany(o => o.Items)
                .HasForeignKey(e => e.OrderId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(e => e.Product)
                .WithMany()
                .HasForeignKey(e => e.ProductId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.Property(x=> x.Quantity)
                .IsRequired()
                .HasDefaultValue(1);

            builder.Property(x => x.UnitPrice)
                .HasColumnType("decimal(18,2)")
                .IsRequired();

            builder.Property(x => x.CreatedAt)
                .IsRequired();
        }
    }
}
