using App.DTO.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Business.Abstract
{
    public interface ICartItemService
    {
        Task<List<CartItemDTO>> CartDetails();
        Task<CartItemDTO> AddtoCart(CartItemDTO cartItem);
        Task UpdateCartQuantity(UpdateCartItemDTO dto);
        Task DeleteCartItem(int productId);
    }
}
