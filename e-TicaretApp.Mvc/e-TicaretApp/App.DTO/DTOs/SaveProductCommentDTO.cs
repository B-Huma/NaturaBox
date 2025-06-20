using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.DTO.DTOs
{
    public class SaveProductCommentDTO
    {
        [Required, MinLength(5), MaxLength(500)]
        public string Text { get; set; }
        [Required, Range(1, 5)]
        public byte StarCount { get; set; }
    }
}
