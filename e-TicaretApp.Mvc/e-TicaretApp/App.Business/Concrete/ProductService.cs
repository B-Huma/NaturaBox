using App.Business.Abstract;
using App.Data.Repositories;
using App.DTO.DTOs;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace App.Business.Concrete
{
    public class ProductService : IProductService
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
        //public async Task Create(ProductCreateDTO dto)
        //{
        //    var response = await _client.PostAsJsonAsync("Product/Create", dto);
        //    if (!response.IsSuccessStatusCode)
        //    {
        //        var error = await response.Content.ReadAsStringAsync();
        //        throw new InvalidOperationException($"{error}");
        //    }
        //}
        public async Task Create(ProductCreateDTO dto)
        {
            using var content = new MultipartFormDataContent();

            content.Add(new StringContent(dto.Name), nameof(dto.Name));
            content.Add(new StringContent(dto.Details), nameof(dto.Details));
            content.Add(new StringContent(dto.CategoryId.ToString()), nameof(dto.CategoryId));
            content.Add(new StringContent(dto.Price.ToString()), nameof(dto.Price));
            content.Add(new StringContent(dto.StockAmount.ToString()), nameof(dto.StockAmount));

            if (!string.IsNullOrEmpty(dto.ImageUrl))
            {
                content.Add(new StringContent(dto.ImageUrl), nameof(dto.ImageUrl));
            }

            var response = await _client.PostAsync("Product/Create", content);

            if (!response.IsSuccessStatusCode)
            {
                var error = await response.Content.ReadAsStringAsync();
                throw new InvalidOperationException($"{error}");
            }
        }

        public async Task Update(int id, ProductUpdateDTO dto)
        {
            using var content = new MultipartFormDataContent();

            content.Add(new StringContent(dto.Id.ToString()), nameof(dto.Id));
            content.Add(new StringContent(dto.Name), nameof(dto.Name));
            content.Add(new StringContent(dto.Details), nameof(dto.Details));
            content.Add(new StringContent(dto.CategoryId.ToString()), nameof(dto.CategoryId));
            content.Add(new StringContent(dto.Price.ToString()), nameof(dto.Price));
            content.Add(new StringContent(dto.StockAmount.ToString()), nameof(dto.StockAmount));

            if (!string.IsNullOrEmpty(dto.ImageUrl))
            {
                content.Add(new StringContent(dto.ImageUrl), nameof(dto.ImageUrl));
            }

            if (dto.ImageFile != null)
            {
                var ms = new MemoryStream();
                await dto.ImageFile.CopyToAsync(ms);
                ms.Position = 0;

                var streamContent = new StreamContent(ms);
                streamContent.Headers.ContentType = new MediaTypeHeaderValue(dto.ImageFile.ContentType);
                content.Add(streamContent, nameof(dto.ImageFile), dto.ImageFile.FileName);

            }

            var response = await _client.PutAsync($"Product/edit/{id}", content);

            if (!response.IsSuccessStatusCode)
            {
                var error = await response.Content.ReadAsStringAsync();
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
