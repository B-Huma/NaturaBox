namespace e_TicaretApp.Mvc.Models.ViewModels
{
    public class OrderItemViewModel
    {
        public string ProductName { get; set; }
        public string ImageUrl { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
    }
}
