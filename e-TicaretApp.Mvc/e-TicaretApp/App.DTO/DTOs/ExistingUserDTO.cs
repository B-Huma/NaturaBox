using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.DTO.DTOs
{
    public class ExistingUserDTO
    {
        public int Id { get; set; }
        [Required, EmailAddress]
        public string Email { get; set; } = default!;
        [Required, MinLength(2)]
        public string FirstName { get; set; } = default!;
        [Required, MinLength(2)]
        public string LastName { get; set; } = default!;
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public bool Enabled { get; set; } = false;
        public string Role { get; set; } = null!;
    }
}
