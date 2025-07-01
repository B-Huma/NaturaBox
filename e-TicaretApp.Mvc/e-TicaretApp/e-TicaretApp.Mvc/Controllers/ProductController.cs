using App.Business.Abstract;
using App.Data.Data;
using App.Data.Data.Entities;
using App.Data.Repositories;
using App.DTO.DTOs;
using AutoMapper;
using e_TicaretApp.Mvc.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Net.Http;
using System.Security.Claims;
using System.Threading.Tasks;

namespace e_TicaretApp.Mvc.Controllers
{
    public class ProductController : Controller
    {
        private readonly HttpClient _fileApiClient;
        private readonly ICategoryService _category;
        private readonly IProductCommentService _comment;
        private readonly IMapper _mapper;
        private readonly IProductService _productService;
        private readonly HttpClient _httpClient;

        public ProductController(IProductService productService, IProductCommentService comment, ICategoryService category, IMapper mapper, IHttpClientFactory factory)
        {
            _fileApiClient = factory.CreateClient("api-file");
            _category = category;
            _comment = comment;
            _mapper = mapper;
            _productService = productService;
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> Create()
        {
            var model = new ProductCreateViewModel();
            var categories = await _category.GetCategories();
            model.Categories = categories.Select(c => new SelectListItem
            {
                Value = c.Id.ToString(),
                Text = c.Name
            }).ToList();
            return View(model);
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Create(ProductCreateViewModel model)
        {
            if (!ModelState.IsValid)
            {
                // Kategorileri tekrar doldur
                var categories = await _category.GetCategories();
                model.Categories = categories.Select(c => new SelectListItem
                {
                    Value = c.Id.ToString(),
                    Text = c.Name
                }).ToList();
                return View(model);
            }

            string? imageUrl = null;
            if (model.ImageFile != null && model.ImageFile.Length > 0)
            {
                using var content = new MultipartFormDataContent();
                using var fileStream = model.ImageFile.OpenReadStream();
                content.Add(new StreamContent(fileStream), "file", model.ImageFile.FileName);

                // File API'ya gönder
                var fileApiResponse = await _fileApiClient.PostAsync("File/upload", content);
                fileApiResponse.EnsureSuccessStatusCode();
                var uploadResult = await fileApiResponse.Content.ReadFromJsonAsync<UploadResponse>();
                imageUrl = uploadResult?.url;

                // Loglama
                Console.WriteLine($"UploadResult: {{ url = {uploadResult?.url} }}");
                Console.WriteLine($"imageUrl: {imageUrl}");
            }

            var dto = _mapper.Map<ProductCreateDTO>(model);
            dto.ImageUrl = imageUrl;

            await _productService.Create(dto);
            return RedirectToAction("MyProducts", "Profile");
        }

        [Authorize]
        [HttpGet("/product/edit/{id}")]
        public async Task<IActionResult> Edit(int id)
        {
            var dto = await _productService.GetProductDetail(id);
            var viewModel = _mapper.Map<ProductUpdateViewModel>(dto);
            
            // Load categories for the dropdown
            var categories = await _category.GetCategories();
            viewModel.Categories = categories.Select(c => new SelectListItem
            {
                Value = c.Id.ToString(),
                Text = c.Name
            }).ToList();
            
            return View(viewModel);
        }

        [Authorize]
        [HttpPost("/product/edit/{id}")]
        public async Task<IActionResult> Edit(int id, ProductUpdateViewModel product)
        {
            string? imageUrl = product.ImageUrl;

            if (product.ImageFile != null && product.ImageFile.Length > 0)
            {
                using var content = new MultipartFormDataContent();
                using var fileStream = product.ImageFile.OpenReadStream();
                content.Add(new StreamContent(fileStream), "file", product.ImageFile.FileName);

                // File API'ya gönder
                var fileApiResponse = await _fileApiClient.PostAsync("File/upload", content);
                fileApiResponse.EnsureSuccessStatusCode();
                var uploadResult = await fileApiResponse.Content.ReadFromJsonAsync<UploadResponse>();
                imageUrl = uploadResult?.url;
            }

            var dto = _mapper.Map<ProductUpdateDTO>(product);
            dto.ImageUrl = imageUrl;

            await _productService.Update(id, dto);
            TempData["SuccessMessage"] = "Product Updated Successfully";
            return RedirectToAction("MyProducts", "Profile");
        }

        [Authorize]
        [Route("/deleteProduct/{id}")]
        [HttpGet]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            await _productService.DeleteProduct(id);
            return RedirectToAction("MyProducts", "Profile");
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Comment(int productId, SaveProductCommentViewModel model)
        {
            var dto = _mapper.Map<SaveProductCommentDTO>(model);
            await _comment.AddComment(productId, dto);            
            return Ok();
        }


    }
    public class UploadResponse
    {
        public string url { get; set; }
    }

}
