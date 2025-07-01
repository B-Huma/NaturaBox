using App.Business.Abstract;
using App.Data.Data.Entities;
using App.Data.Repositories;
using App.DTO.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace App.Business.Concrete
{
    public class UserService : IUserService
    {
        private readonly HttpClient _client;

        public UserService(IHttpClientFactory factory)
        {
            _client = factory.CreateClient("data-api");
        }

        public async Task<List<AdminUserDTO>> GetNonAdminUsers()
        {
            var response = await _client.GetAsync("User/users");
            if (!response.IsSuccessStatusCode)
            {
                throw new InvalidOperationException();
            }
            return await response.Content.ReadFromJsonAsync<List<AdminUserDTO>>();
        }
        public async Task<List<AdminUserDTO>> GetUnapprovedUsers()
        {
            var response = await _client.GetAsync("User/UnapprovedUsers");
            if (!response.IsSuccessStatusCode)
            {
                throw new InvalidOperationException();
            }
            return await response.Content.ReadFromJsonAsync<List<AdminUserDTO>>();
        }
        public async Task ApproveUser(int id)
        {
            var response = await _client.PutAsync($"User/Update/{id}", null);
            if (!response.IsSuccessStatusCode)
            {
                throw new InvalidOperationException();
            }
        }
        public async Task DeleteRequest(int id)
        {
            var response = await _client.DeleteAsync($"User/Delete/{id}");
            if (!response.IsSuccessStatusCode)
            {
                throw new InvalidOperationException();
            }
        }
    }
}
