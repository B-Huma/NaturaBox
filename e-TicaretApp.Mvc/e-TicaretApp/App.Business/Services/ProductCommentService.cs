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
    public class ProductCommentService
    {
        private readonly IAdminCommentRepository _repo;
        private readonly HttpClient _client;

        public ProductCommentService(IHttpClientFactory factory)
        {
            _client = factory.CreateClient("data-api");
        }
        public async Task<List<AdminProductCommentDTO>> ProductComments()
        {
            var response = await _client.GetAsync("Comment/comment");
            if (!response.IsSuccessStatusCode)
            {
                throw new InvalidOperationException();
            }
            var responseObject = await response.Content.ReadFromJsonAsync<List<AdminProductCommentDTO>>();
            return responseObject;
        }
        public async Task<List<AdminProductCommentDTO>> UnapprovedComments()
        {
            var response = await _client.GetAsync("Comment/comment/unapproved");
            if (!response.IsSuccessStatusCode)
            {
                throw new InvalidOperationException();
            }
            var responseObject = await response.Content.ReadFromJsonAsync<List<AdminProductCommentDTO>>();
            return responseObject;                      
        }
        public async Task Approve(int id)
        {
            var request = new HttpRequestMessage(HttpMethod.Put, $"Comment/ApproveComment/{id}")
            {
                Content = null
            };
            var response = await _client.SendAsync(request);
            response.EnsureSuccessStatusCode();
        }
        public async Task Delete(int id)
        {
            var response = await _client.DeleteAsync($"Comment/Delete/{id}");
        }
        public async Task AddComment(int productId, SaveProductCommentDTO comment)
        {
            var response = await _client.PostAsJsonAsync($"Comment/ProductComment/{productId}", comment);
            if (!response.IsSuccessStatusCode)
            {
                throw new InvalidOperationException();
            }
        }
    }
}
