using App.DTO.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace App.Business.Services
{
    public class AuthService
    {
        private readonly HttpClient _client;

        public AuthService(IHttpClientFactory factory)
        {
            _client = factory.CreateClient("data-api");
        }
        public async Task<LoginResponseDTO?> LoginRequestAsync(LoginRequestDTO dto)
        {
            var response = await _client.PostAsJsonAsync("Auth/Login", dto);

            if (!response.IsSuccessStatusCode)
            {
                return null;
            }

            return await response.Content.ReadFromJsonAsync<LoginResponseDTO>();
        }


        public async Task RegisterAsync(RegisterRequestDTO dto)
        {
            var response = await _client.PostAsJsonAsync("Auth/Register", dto);

            if (!response.IsSuccessStatusCode)
            {
                var error = await response.Content.ReadAsStringAsync();
                throw new InvalidOperationException($"Registeration failed: {error}");
            }
        }
        public async Task ChangePassword(ChangePasswordDTO dto)
        {
            var response = await _client.PutAsJsonAsync("Auth/RenewPassword",dto);

            if (!response.IsSuccessStatusCode)
            {
                var error = await response.Content.ReadAsStringAsync();
                throw new InvalidOperationException($"{error}");
            }
        }
        public async Task Logout()
        {
            var response = await _client.PostAsync("Auth/Logout",null);

            if (!response.IsSuccessStatusCode)
            {
                var error = await response.Content.ReadAsStringAsync();
                throw new InvalidOperationException($"{error}");
            }
        }
    }
}
