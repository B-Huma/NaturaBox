using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.DTO.DTOs
{
    public class OrderCreateDTO
    {
        public int UserId { get; set; }
        public string Address { get; set; }
        public string FullName { get; set; }
    }
}
