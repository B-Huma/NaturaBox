using System.ComponentModel.DataAnnotations;

namespace e_TicaretApp.Mvc.Models.ViewModels
{
    public class RegisterViewModel
    {
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Password { get; set; }
    }
}
