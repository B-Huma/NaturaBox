using App.Business.Services;
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
using System.Security.Claims;
using System.Threading.Tasks;

namespace e_TicaretApp.Mvc.Controllers
{
    public class ProductController : Controller
    {
        private readonly CategoryService _category;
        private readonly ProductCommentService _comment;
        private readonly IMapper _mapper;
        private readonly ProductService _productService;

        public ProductController(ProductService productService, ProductCommentService comment, CategoryService category, IMapper mapper)
        {
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
                var categories = await _category.GetCategories();
                model.Categories = categories.Select(c => new SelectListItem
                {
                    Value = c.Id.ToString(),
                    Text = c.Name
                }).ToList();
                return View(model);
            }

            var dto = _mapper.Map<ProductCreateDTO>(model);
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
            var dto = _mapper.Map<ProductUpdateDTO>(product);
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
}
