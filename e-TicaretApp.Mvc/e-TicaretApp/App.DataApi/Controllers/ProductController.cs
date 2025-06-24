using App.Business.Services;
using App.Data.Data.Entities;
using App.Data.Repositories;
using App.DTO.DTOs;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace App.DataApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IUserRepository _user;
        private readonly IMapper _mapper;
        private readonly IAdminCategoryRepository _category;
        private readonly IProductRepository _repo;
        private readonly FileApiService _fileApiService;

        public ProductController(IProductRepository repo, IAdminCategoryRepository category, IUserRepository user, IMapper mapper)
        {
            _user = user;
            _mapper = mapper;
            _category = category;
            _repo = repo;
        }
        [HttpGet("ShopListing")]
        public async Task<ShopDTO> ShopListing()
        {
            var products = await _repo.GetAllWithCategoryAndSeller();
            var categories = await _category.GetCategoriesAsync();

            var productDTO = _mapper.Map<List<ProductDTO>>(products);
            var categoryDTO = _mapper.Map<List<CategoryDTO>>(categories);
            return new ShopDTO
            {
                Products = productDTO,
                Categories = categoryDTO
            };
        }
        [HttpGet("product-detail/{id}")]
        public async Task<IActionResult> GetProductDetail(int id)
        {
            var product = await _repo.GetProductWithDetailsAsync(id);
            if (product == null)
            {
                return NotFound();
            }

            var dto = _mapper.Map<ProductDetailDTO>(product);
            return Ok(dto);
        }
        [HttpPost("Create")]
        public async Task<IActionResult> Create(ProductCreateDTO dto)
        {
            if (dto == null) return BadRequest();
            var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("", "Invalid product information.");
            }
            var user = await _user.UserIdExists(userId);
            var category = await _category.CategoryExists(dto.CategoryId);
            if (user == null || category == null)
            {
                ModelState.AddModelError("", "User or category not found");
            }
            string? image = null;
            if (dto.ImageFile != null)
            {
                image = await _fileApiService.UploadFileAsync(dto.ImageFile);
            }
            var product = new ProductEntity
            {
                Name = dto.Name,
                Details = dto.Details,
                Price = dto.Price,
                CategoryId = dto.CategoryId,
                SellerId = userId,
                StockAmount = dto.StockAmount,
                CreatedAt = DateTime.Now,
                Enabled = true,
                Images = new List<ProductImageEntity>
                {
                    new ProductImageEntity
                    {
                        Url = image ?? "/assets/img/default-product.png"
                    }
                }
            };
            await _repo.AddAsync(product);
            return StatusCode(201);
        }
        [HttpPut("edit/{id}")]
        public async Task<IActionResult> Edit(int id, ProductUpdateDTO dto)
        {
            var existing = await _repo.GetByIdAsync(id);
            if (existing == null) return NotFound();

            var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));

            existing.Name = dto.Name;
            existing.Details = dto.Details;
            existing.Price = dto.Price;
            existing.CategoryId = dto.CategoryId;
            existing.StockAmount = dto.StockAmount;

            if (dto.ImageFile != null)
            {
                var newImageUrl = await _fileApiService.UploadFileAsync(dto.ImageFile);
                existing.Images.Clear();
                existing.Images.Add(new ProductImageEntity { Url = newImageUrl });
            }

            await _repo.UpdateAsync(existing);
            return NoContent();
        }
        [HttpDelete("DeleteProduct/{id}")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            var product = await _repo.GetProduct(id);
            if (product == null) return NotFound();
            await _repo.DeleteProduct(id);
            return NoContent();
        }
    }
}
