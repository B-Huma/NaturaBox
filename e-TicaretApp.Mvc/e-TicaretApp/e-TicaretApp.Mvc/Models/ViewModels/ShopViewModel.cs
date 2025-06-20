using App.Data.Data.Entities;

namespace e_TicaretApp.Mvc.Models.ViewModels
{
    public class ShopViewModel
    {
        public List<ProductViewModel> Products { get; set; }
        public List<CategoryViewModel> Categories { get; set; }
    }

    public class ProductViewModel
    {
        public int Id { get; set; }
        public int SellerId { get; set; }
        public int CategoryId { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public string Details { get; set; }
        public List<ImageViewModel> Images { get; set; } = new();
    }

    public class CategoryViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Color { get; set; }
        public string IconCssClass { get; set; }
        public DateTime CreatedAt { get; set; }
    }
    public class ImageViewModel
    {
        public string Url { get; set; } = string.Empty;
    }



}
