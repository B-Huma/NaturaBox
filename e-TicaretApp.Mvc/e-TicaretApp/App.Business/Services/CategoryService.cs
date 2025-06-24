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
    public class CategoryService 
    {
        private readonly HttpClient _client;

        public CategoryService(IHttpClientFactory factory)
        {
            _client = factory.CreateClient("data-api");
        }        
        public async Task<List<CategoryDTO>> GetCategories()
        {
            var response = await _client.GetAsync("Category/categoryList");
            if (!response.IsSuccessStatusCode)
            {
                throw new InvalidOperationException();
            }
            var responseObject = await response.Content.ReadFromJsonAsync<List<CategoryDTO>>();
            return responseObject;

        }
        public async Task<CategoryDTO> GetCategoryById(int id)
        {
            var response = await _client.GetFromJsonAsync<CategoryDTO>($"Category/{id}"); 
            return response;              
        }
        public async Task CreateCategory(CategoryDTO dto)
        {
            if (dto == null || string.IsNullOrWhiteSpace(dto.Name))
            {
                throw new Exception("Please fill all the necessary parts");
            }
            var response = await _client.PostAsJsonAsync("Category/Create", dto);
            response.EnsureSuccessStatusCode();
        }
        public async Task EditCategory(int id, CategoryDTO dto)
        {
            if (id != dto.Id)
            {
                throw new Exception("Could not find the category");
            }
            var response = await _client.PutAsJsonAsync($"Category/Update/{id}", dto);
            if (!response.IsSuccessStatusCode)
            {
                throw new InvalidProgramException();
            }
            response.EnsureSuccessStatusCode();

        }
        public async Task DeleteCategory(int id)
        {
            var response = await _client.DeleteAsync($"Category/Delete/{id}");
            response.EnsureSuccessStatusCode();
        }

    }
}
