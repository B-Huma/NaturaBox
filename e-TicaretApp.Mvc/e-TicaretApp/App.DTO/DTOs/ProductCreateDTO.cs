using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.DTO.DTOs
{
    public class ProductCreateDTO
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "There must a name")]
        [StringLength(100, MinimumLength = 3, ErrorMessage = "Product name's length must be between 3-100")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Details is necessary")]
        public string Details { get; set; }

        [Range(0.1, double.MaxValue, ErrorMessage = "Invalid price")]
        public decimal Price { get; set; }

        [Required(ErrorMessage = "Must choose a category")]
        public int CategoryId { get; set; }

        [Range(1, 255, ErrorMessage = "Stock must be between 1-255")]
        public byte StockAmount { get; set; }

        public IFormFile? ImageFile { get; set; }
    }

}
