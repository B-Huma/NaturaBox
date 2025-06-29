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
    public class UserEntity
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [EmailAddress]
        public string Email { get; set; } = default!;
        [MinLength(2)]
        public string FirstName { get; set; } = default!;
        [MinLength(2)]
        public string LastName { get; set; } = default!;
        public string Password { get; set; } = default!;
        public int RoleId { get; set; }
        public bool Enabled { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public RoleEntity Role { get; set; } = default!;
        
        public ICollection<OrderEntity> Orders { get; set; } = new List<OrderEntity>();
        public ICollection<CartItemEntity> CartItems { get; set; } = new List<CartItemEntity>();
    }
    public class UserEntityConfiguration : IEntityTypeConfiguration<UserEntity>
    {
        public void Configure(EntityTypeBuilder<UserEntity> builder)
        {
            builder.Property(nameof(UserEntity.Email))
                .IsRequired();
            builder.Property(nameof(UserEntity.FirstName))
                .IsRequired()
                .HasMaxLength(50);
            builder.Property(nameof(UserEntity.LastName))
                .IsRequired()
                .HasMaxLength(50);
            builder.Property(nameof(UserEntity.Password))
                .IsRequired();
            builder.Property(nameof(UserEntity.RoleId))
                .IsRequired();
            builder.Property(nameof(UserEntity.Enabled))
                .IsRequired()
                .HasDefaultValue(false);
            builder.Property(nameof(UserEntity.CreatedAt))
                .IsRequired();

            builder.HasOne(u => u.Role)
                .WithMany()
                .HasForeignKey(u => u.RoleId)
                .OnDelete(DeleteBehavior.NoAction);
            

        }
    }
}
