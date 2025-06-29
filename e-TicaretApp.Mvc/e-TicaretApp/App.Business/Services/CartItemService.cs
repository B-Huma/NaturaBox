using App.Data.Data.Entities;
using App.Data.Repositories;
using App.DTO.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace App.Business.Services
{
    public class CartItemService
    {
        private readonly HttpClient _client;

        public CartItemService(IHttpClientFactory factory)
        {
            _client = factory.CreateClient("data-api");
        }

        public async Task<List<CartItemDTO>> CartDetails()
        {
            var response = await _client.GetAsync("CartItem/CartDetails");
            if (!response.IsSuccessStatusCode)
            {
                return new List<CartItemDTO>();
            }
            var cartItems = await response.Content.ReadFromJsonAsync<List<CartItemDTO>>();
            return cartItems ?? new List<CartItemDTO>();
        }
        public async Task<CartItemDTO> AddtoCart(CartItemDTO cartItem)
        {
            var response = await _client.PostAsJsonAsync("CartItem/Add", cartItem);
            if (!response.IsSuccessStatusCode)
            {
                throw new Exception("Please login first to add a product to your cart.");
            }
            return await response.Content.ReadFromJsonAsync<CartItemDTO>();
        }
        public async Task UpdateCartQuantity(UpdateCartItemDTO dto)
        {
            var response = await _client.PutAsJsonAsync("CartItem/Update", dto);
            if (!response.IsSuccessStatusCode)
            {
                var error = await response.Content.ReadAsStringAsync();
                throw new InvalidOperationException($"API hatası: {response.StatusCode}, {error}");
            }
        }
        public async Task DeleteCartItem(int productId)
        {
            var response = await _client.DeleteAsync($"CartItem/{productId}");
            if (!response.IsSuccessStatusCode)
            {
                throw new Exception("Could not delete item from cart.");
            }
        }
    }
}
