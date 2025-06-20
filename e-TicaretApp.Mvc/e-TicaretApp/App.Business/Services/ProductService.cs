using App.Data.Repositories;
using App.DTO.DTOs;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace App.Business.Services
{
    public class ProductService
    {
        private readonly IMapper _mapper;
        private readonly HttpClient _client;

        public ProductService(IHttpClientFactory factory, IMapper mapper)
        {
            _mapper = mapper;
            _client = factory.CreateClient("data-api");
        }
        public async Task<ShopDTO> Get()
        {
            var response = await _client.GetAsync("Product/ShopListing");
            if (!response.IsSuccessStatusCode)
            {
                throw new InvalidOperationException();
            }
            var responseObject = await response.Content.ReadFromJsonAsync<ShopDTO>();
            return responseObject;
        }
        public async Task<ProductDetailDTO> GetProductDetail(int id)
        {
            var response = await _client.GetAsync($"Product/product-detail/{id}");
            if (!response.IsSuccessStatusCode)
            {
                throw new InvalidOperationException();
            }
            var dto = await response.Content.ReadFromJsonAsync<ProductDetailDTO>();
            return dto;
        }
        public async Task Create(ProductCreateDTO dto)
        {
            var response = await _client.PostAsJsonAsync("Product/Create", dto);
            if (!response.IsSuccessStatusCode)
            {
                var error = await response.Content.ReadAsStringAsync();
                throw new InvalidOperationException($"{error}");
            }
        }
        public async Task Update(int id, ProductUpdateDTO dto)
        {
            var response = await _client.PutAsJsonAsync($"Product/edit/{id}", dto);
            if (!response.IsSuccessStatusCode)
            {
                var error = await response.Content.ReadAsStringAsync ();
                throw new InvalidOperationException($"{error}");
            }
        }
        public async Task DeleteProduct(int id)
        {
            var response = await _client.DeleteAsync($"Product/DeleteProduct/{id}");
            if (!response.IsSuccessStatusCode)
            {
                var error = await response.Content.ReadAsStringAsync();
                throw new InvalidOperationException($"{error}");
            }
        }
    }
}
