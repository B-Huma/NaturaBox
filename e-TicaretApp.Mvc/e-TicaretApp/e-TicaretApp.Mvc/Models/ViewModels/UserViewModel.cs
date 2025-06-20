using System.ComponentModel.DataAnnotations;
using App.Data.Data.Entities;

namespace e_TicaretApp.Mvc.Models.ViewModels
{
    public class UserViewModel
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
