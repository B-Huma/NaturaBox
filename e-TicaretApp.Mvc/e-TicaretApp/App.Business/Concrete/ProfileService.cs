using App.Business.Abstract;
using App.DTO.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace App.Business.Concrete
{
    public class ProfileService : IProfileService
    {
        private readonly HttpClient _client;

        public ProfileService(IHttpClientFactory factory)
        {
            _client = factory.CreateClient("data-api");
        }
        public async Task<ExistingUserDTO> GetProfileDetails()
        {
            var response = await _client.GetAsync("Profile/profile-details");
            if (!response.IsSuccessStatusCode)
            {
                return null;
            }
            return await response.Content.ReadFromJsonAsync<ExistingUserDTO>();
        }
        public async Task<bool> UpdateProfile(int id, UserEditDTO dto)
        {
            var response = await _client.PutAsJsonAsync($"Profile/EditProfile/{id}", dto);
            return response.IsSuccessStatusCode;
        }
        public async Task<List<ProductViewDTO>> GetUserProductsAsync()
        {
            var response = await _client.GetFromJsonAsync<List<ProductViewDTO>>("Profile/user-products");
            return response ?? new List<ProductViewDTO>();
        }
        public async Task<string> RequestSeller()
        {
            var response = await _client.PostAsync("Profile/request-seller",null);
            if (!response.IsSuccessStatusCode)
            {
                return "Error submitting seller request";
            }
            var json = await response.Content.ReadFromJsonAsync<Dictionary<string, string>>();
            return json?["message"] ?? "Request processed";
        }
    }
}
