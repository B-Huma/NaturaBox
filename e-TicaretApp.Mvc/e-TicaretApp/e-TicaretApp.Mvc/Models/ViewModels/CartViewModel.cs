namespace e_TicaretApp.Mvc.Models.ViewModels
{
    public class CartViewModel
    {
        // sepetin tamamı için liste ile ürünleri çekiyoruz, carttotal da sepet tutarı için
        public List<CartItemViewModel> Items { get; set; }
        public decimal CartTotal => Items.Sum(x => x.TotalPrice);
    }
}
