namespace e_TicaretApp.Mvc.Models.ViewModels
{
    public class MyProductsViewModel
    {
        public List<ProductTableItem> Products { get; set; }
    }
    public class ProductTableItem
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public string Details { get; set; } = string.Empty;
        public string CategoryName { get; set; } = string.Empty;
        public string ImageUrl { get; set; } = string.Empty;
        public byte StockAmount { get; set; }
    }
}
