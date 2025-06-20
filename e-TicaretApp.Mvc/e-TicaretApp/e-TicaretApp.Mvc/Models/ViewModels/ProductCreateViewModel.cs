using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace e_TicaretApp.Mvc.Models.ViewModels
{
    public class ProductCreateViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Ürün adı gereklidir")]
        [StringLength(100, MinimumLength = 3, ErrorMessage = "Ürün adı 3-100 karakter olmalı")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Detaylar gereklidir")]
        public string Details { get; set; }

        [Range(0.1, double.MaxValue, ErrorMessage = "Geçersiz fiyat")]
        public decimal Price { get; set; }

        [Required(ErrorMessage = "Kategori seçmelisiniz")]
        public int CategoryId { get; set; }

        [Range(1, 255, ErrorMessage = "Stok 1-255 arasında olmalı")]
        public byte StockAmount { get; set; }

        public IFormFile? ImageFile { get; set; }
        public List<SelectListItem> Categories { get; set; } = new();
        public string? CurrentImageUrl { get; set; }
    }
}
