using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace App.Data.Data.Entities
{
    public class CategoryEntity
    {
        public int Id { get; set; }
        [MinLength(2)]
        public string Name { get; set; }
        [MinLength(3)]
        public string Color { get; set; } = default!;
        [MinLength(2)]
        public string IconCssClass { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
    }
    public class CategoryEntityConfiguration : IEntityTypeConfiguration<CategoryEntity>
    {
        public void Configure(EntityTypeBuilder<CategoryEntity> builder)
        {
            builder.HasKey(e => e.Id);
            builder.Property(e => e.Name)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(e => e.Color)
                .IsRequired()
                .HasMaxLength(7);

            builder.Property(e => e.IconCssClass)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(e => e.CreatedAt)
                .IsRequired();

        }
    }
}
