using System.ComponentModel.DataAnnotations;

namespace e_TicaretApp.Mvc.Models.ViewModels
{
    public class OrderCreateViewModel
    {
        public string Adress { get; set; }
        public string Name { get; set; }
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        public string Phone { get; set; }

    }
}
