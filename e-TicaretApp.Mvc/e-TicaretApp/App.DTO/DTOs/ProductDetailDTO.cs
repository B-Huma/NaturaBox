using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.DTO.DTOs
{
    public class ProductDetailDTO
    {
        public int Id { get; set; }
        [Required, MinLength(2), MaxLength(100)]
        public string Name { get; set; } = null!;
        [Required, DataType(DataType.Currency)]
        public decimal Price { get; set; }
        [Required, MaxLength(1000)]
        public string Details { get; set; } = null!;
        [Required]
        public string? ImageUrl { get; set; }
        public string? CategoryName { get; set; }
        public int CategoryId { get; set; }
        public byte StockAmount { get; set; }

        public List<ProductCommentDTO> Comments { get; set; }
    }
}
