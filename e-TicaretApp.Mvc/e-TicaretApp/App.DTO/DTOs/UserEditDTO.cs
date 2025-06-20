using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.DTO.DTOs
{
    public class UserEditDTO
    {
        public int Id { get; set; }
        [MinLength(2)]
        [Required, MaxLength(50)]
        public string FirstName { get; set; }
        [MinLength(2)]
        [Required, MaxLength(50)]
        public string LastName { get; set; }
        [EmailAddress, Required]
        public string Email { get; set; }
    }
}
