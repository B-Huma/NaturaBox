using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.DTO.DTOs
{
    public class ProductCommentDTO
    {
        [Required]
        public int Id { get; set; }
        [Required, StringLength(500, MinimumLength = 2)]
        public string Text { get; set; } = null!;
        [Required, Range(1, 5)]
        public byte StarCount { get; set; }
        public string UserName { get; set; } = null!;
    }
}
