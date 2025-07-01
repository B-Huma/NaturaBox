using System.ComponentModel.DataAnnotations;

namespace e_TicaretApp.Mvc.Models.ViewModels
{
    public class OrderCreateViewModel
    {
        public string Address { get; set; }
        public string FullName { get; set; }
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        public string Phone { get; set; }

    }
}
