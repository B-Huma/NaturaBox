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
    public class ProductImageEntity
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public int ProductId { get; set; }

        [DataType(DataType.Url), MinLength(10)]
        public string Url { get; set; }
        public DateTime CreatedAt { get; set; }

        [ForeignKey(nameof(ProductId))]
        public ProductEntity Product { get; set; }
    }
    public class ProductImageEntityConfiguration : IEntityTypeConfiguration<ProductImageEntity>
    {
        public void Configure(EntityTypeBuilder<ProductImageEntity> builder)
        {
            builder.Property(nameof(ProductImageEntity.ProductId))
                .IsRequired();
            builder.Property(nameof(ProductImageEntity.Url))
                .IsRequired()
                .HasMaxLength(250);
            builder.Property(nameof(ProductImageEntity.CreatedAt))
                .IsRequired();

            builder.HasOne(pi => pi.Product)
                 .WithMany(p=> p.Images)
                 .HasForeignKey(pi => pi.ProductId)
                 .OnDelete(DeleteBehavior.Cascade);

        }
    }
}
