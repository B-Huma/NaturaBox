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
        [MinLength(2)]
        public string Name { get; set; } = default!;
        [MinLength(3)]
        public string Color { get; set; } = default!;
        [MinLength(2)]
        public string IconCssClass { get; set; } = default!;
        public DateTime CreatedAt { get; set; } = DateTime.Now;
    }
}
