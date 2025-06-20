using System.ComponentModel.DataAnnotations.Schema;

namespace e_TicaretApp.Mvc.Models.ViewModels
{
    public class CartItemViewModel
    {

        // sepet sayfasında görüntülemek için
        public int Id { get; set; }
        public string ProductName { get; set; } = string.Empty;
        public decimal UnitPrice { get; set; }
        public byte Quantity { get; set; }
        public decimal TotalPrice {  get; set; }
        public int ProductId { get; set; }
    }
}
