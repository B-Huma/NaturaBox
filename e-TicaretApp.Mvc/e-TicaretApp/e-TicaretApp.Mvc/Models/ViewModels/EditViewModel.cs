using System.ComponentModel.DataAnnotations;

namespace e_TicaretApp.Mvc.Models.ViewModels
{
    public class EditViewModel
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
