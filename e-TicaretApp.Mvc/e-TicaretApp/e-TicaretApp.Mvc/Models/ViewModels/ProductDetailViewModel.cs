using System.ComponentModel.DataAnnotations;

namespace e_TicaretApp.Mvc.Models.ViewModels
{
    public class ProductDetailViewModel
    {
        public int Id { get; set; }
        [Required, MinLength(2), MaxLength(100)]
        public string Name { get; set; } = null!;
        [Required,DataType(DataType.Currency)]
        public decimal Price { get; set; }
        [Required,MaxLength(1000)]
        public string Details { get; set; } = null!;
        [Required]
        public string? ImageUrl { get; set; }
        public string? CategoryName { get; set; }

        public List<ProductCommentViewModel> Comments { get; set; }

    }
}
