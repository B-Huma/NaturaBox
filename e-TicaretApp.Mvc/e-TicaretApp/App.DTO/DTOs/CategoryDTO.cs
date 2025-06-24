using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.DTO.DTOs
{
    public class CategoryDTO
    {
        public int Id { get; set; }
        public string Name { get; set; } = default!;
        public string Color { get; set; } = default!;
        public string IconCssClass { get; set; } = default!;
        public DateTime CreatedAt { get; set; } = DateTime.Now;
    }
}
