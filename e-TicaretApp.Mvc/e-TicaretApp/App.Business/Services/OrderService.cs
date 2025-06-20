using App.DTO.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace App.Business.Services
{
    public class OrderService
    {
        private readonly HttpClient _client;

        public OrderService(IHttpClientFactory factory)
        {
            _client = factory.CreateClient("data-api");
        }
        public async Task<string> CreateOrder(OrderDTO dto)
        {
            var response = await _client.PostAsJsonAsync("Order/CreateOrder", dto);
            if (!response.IsSuccessStatusCode)
            {
                var error = await response.Content.ReadAsStringAsync();
                throw new InvalidOperationException(error);
            }

            var result = await response.Content.ReadFromJsonAsync<OrderResultDTO>();
            return result.OrderCode;
        }

        public async Task<List<OrderDTO>> OrderDetails()
        {
            var response = await _client.GetAsync("OrderDetails/OrderDetails");
            if (!response.IsSuccessStatusCode)
            {
                throw new InvalidOperationException();
            }
            return await response.Content.ReadFromJsonAsync<List<OrderDTO>>();
        }
        
    }
}
