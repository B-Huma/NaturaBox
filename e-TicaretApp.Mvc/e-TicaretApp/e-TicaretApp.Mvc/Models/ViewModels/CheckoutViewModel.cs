namespace e_TicaretApp.Mvc.Models.ViewModels
{
    public class CheckoutViewModel
    {
        public int Id { get; set; }
        public string ProductName { get; set; } = null!;
        public decimal Price { get; set; } 
        public byte Quantity { get; set; }
    }
}
