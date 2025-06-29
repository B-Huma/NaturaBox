using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.DTO.DTOs
{
    public class ProductUpdateDTO
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Details { get; set; }

        [Range(0.1, double.MaxValue)]
        public decimal Price { get; set; }

        [Required]
        public int CategoryId { get; set; }

        [Range(1, 255)]
        public byte StockAmount { get; set; }

        public string? ImageUrl { get; set; } 

        public IFormFile? ImageFile { get; set; } 
    }
}
