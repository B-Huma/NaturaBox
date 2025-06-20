using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace App.Data.Data.Entities
{
    public class ProductEntity
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public int SellerId { get; set; }
        public int CategoryId { get; set; }
        [MinLength(2)]
        public string Name { get; set; } = null!;
        [DataType(DataType.Currency)]
        [Column(TypeName = "decimal(18,2)")]
        public decimal Price { get; set; }
        public string Details { get; set; } = null!;
        public byte StockAmount { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime UpdatedAt {  get; set; } = DateTime.Now;
        public bool Enabled { get; set; }       
        
        public UserEntity Seller { get; set; } = null!;        
        public CategoryEntity Category { get; set; } = null!;
        public ICollection<ProductImageEntity> Images { get; set; } = new List<ProductImageEntity>();
        public ICollection<ProductCommentEntity> Comments { get; set; } = new List<ProductCommentEntity>();

        public ICollection<CartItemEntity> CartItems { get; set; } = new List<CartItemEntity>();

    }
    public class ProductEntityConfiguration : IEntityTypeConfiguration<ProductEntity>
    {
        public void Configure(EntityTypeBuilder<ProductEntity> builder)
        {
            builder.Property(nameof(ProductEntity.SellerId))
                .IsRequired();
            builder.Property(nameof(ProductEntity.CategoryId))
                .IsRequired();
            builder.Property(nameof(ProductEntity.Name))
                .IsRequired()
                .HasMaxLength(100);
            builder.Property(nameof(ProductEntity.Price))
                .IsRequired();
            builder.Property(nameof(ProductEntity.Details))
                .HasMaxLength(1000);
            builder.Property(nameof(ProductEntity.StockAmount))
                .IsRequired();
            builder.Property(nameof(ProductEntity.CreatedAt))
                .IsRequired();
            builder.Property(nameof(ProductEntity.Enabled))
                .IsRequired()
                .HasDefaultValue(true);

            builder.HasOne(p => p.Seller)
                .WithMany()
                .HasForeignKey(p => p.SellerId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.HasOne(p => p.Category)
                .WithMany()
                .HasForeignKey(p => p.CategoryId)
                .OnDelete(DeleteBehavior.NoAction);


        }
    }
}
