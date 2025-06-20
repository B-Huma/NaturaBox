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
    public class CartItemEntity
    {
        // sepetteki ürünler
        public int Id { get; set; }
        public int UserId { get; set; }
        public int ProductId { get; set; }
        [Range(1, 255, ErrorMessage = "Quantity must be at least 1.")]
        public byte Quantity { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;

        
        public UserEntity User { get; set; } = null!;

       
        public ProductEntity Product { get; set; } = null!;
    }
    public class CartItemEntityConfiguration : IEntityTypeConfiguration<CartItemEntity>
    {
        public void Configure(EntityTypeBuilder<CartItemEntity> builder)
        {
            builder.HasKey(e => e.Id);
            builder.Property(e => e.UserId).IsRequired();
            builder.Property(e => e.ProductId).IsRequired();
            builder.Property(e => e.Quantity).IsRequired().HasDefaultValue(1); // 1 item by default
            builder.Property(e => e.CreatedAt).IsRequired();

            builder.HasOne(x => x.User)
                .WithMany()
                .HasForeignKey(x => x.UserId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.HasOne(x => x.Product)
                .WithMany()
                .HasForeignKey(x => x.ProductId)
                .OnDelete(DeleteBehavior.NoAction);

            
        }
    }
}
