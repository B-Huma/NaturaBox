using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.DTO.DTOs
{
    public class UpdateCartItemDTO
    {
        public int UserId { get; set; }
        public int ProductId { get; set; }
        public byte Quantity { get; set; }
    }
}
