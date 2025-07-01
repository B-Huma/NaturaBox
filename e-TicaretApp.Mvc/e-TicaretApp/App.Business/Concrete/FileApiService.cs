using App.Business.Abstract;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace App.Business.Concrete
{
    public class FileApiService : IFileApiService
    {
        private readonly HttpClient _client;

        public FileApiService(IHttpClientFactory factory)
        {
            _client = factory.CreateClient("api-file");
        }

        public async Task<string?> UploadFileAsync(IFormFile file)
        {
            if (file == null)
                throw new ArgumentNullException(nameof(file), "Dosya null gönderildi");
            using var content = new MultipartFormDataContent();

            using var ms = new MemoryStream();
            await file.CopyToAsync(ms);
            ms.Position = 0;

            var fileContent = new StreamContent(ms);
            fileContent.Headers.ContentType = new MediaTypeHeaderValue(file.ContentType);
            content.Add(fileContent, "file", file.FileName);

            var response = await _client.PostAsync("File/upload", content);

            if (!response.IsSuccessStatusCode)
                return null;

            var result = await response.Content.ReadFromJsonAsync<UploadResponse>();
            return result?.Url;
        }

        public async Task<Stream?> DownloadFileAsync(string fileName)
        {
            var response = await _client.GetAsync($"File/download?fileName={fileName}");

            if (!response.IsSuccessStatusCode)
                return null;

            return await response.Content.ReadAsStreamAsync();
        }

        private class UploadResponse
        {
            public string Url { get; set; }
        }
    }
}
