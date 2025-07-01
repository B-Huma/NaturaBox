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
    public class OrderEntity
    {
        // siparişin son hali
        public int Id { get; set; }
        public int UserId { get; set; }
        [MinLength(2)]
        public string OrderCode { get; set; }
        [MinLength(2)]
        public string Address { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;        
        
        public UserEntity User { get; set; }
        public ICollection<OrderItemEntity> Items { get; set; } = new List<OrderItemEntity>();
    }
    public class OrderEntityConfiguration : IEntityTypeConfiguration<OrderEntity>
    {
        public void Configure(EntityTypeBuilder<OrderEntity> builder)
        {
            builder.HasKey(e => e.Id);

            builder.Property(e => e.UserId)
                .IsRequired();

            builder.Property(e => e.OrderCode)
                .IsRequired()
                .HasMaxLength(250);

            builder.Property(e => e.Address)
                .IsRequired()
                .HasMaxLength(250);

            builder.Property(e => e.CreatedAt)
                .IsRequired();

            builder.HasOne(d => d.User)
                .WithMany(u => u.Orders)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.NoAction);
                
            builder.HasMany(d => d.Items)
                .WithOne(i => i.Order)
                .HasForeignKey(i => i.OrderId)
                .OnDelete(DeleteBehavior.Cascade);
                
            builder.Property(e => e.Id)
                .ValueGeneratedOnAdd();
        }
    }
}
