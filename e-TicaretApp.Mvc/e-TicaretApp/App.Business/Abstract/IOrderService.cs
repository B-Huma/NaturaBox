using App.DTO.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Business.Abstract
{
    public interface IOrderService
    {
        Task<string> CreateOrder(OrderCreateDTO dto);
        Task<List<OrderDTO>> OrderDetails();
    }
}
