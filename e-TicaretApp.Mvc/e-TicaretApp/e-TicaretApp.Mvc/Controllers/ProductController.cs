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
        private readonly ProductCommentService _comment;
        private readonly IMapper _mapper;
        private readonly ProductService _productService;

        public ProductController(ProductService productService, ProductCommentService comment, IMapper mapper)
        {
            _comment = comment;
            _mapper = mapper;
            _productService = productService;
        }

        [Authorize]
        [Route("/create")]
        [HttpGet]
        public IActionResult Create()
        {
            var model = new ProductCreateViewModel();
            return View(model);
        }

        [Authorize]
        [Route("/create")]
        [HttpPost]
        public async Task<IActionResult> Create(ProductCreateViewModel model)
        {
            var dto = _mapper.Map<ProductCreateDTO>(model);
            await _productService.Create(dto);
            return View(model);
        }

        [Authorize]
        [HttpGet("/product/edit/{id}")]
        public async Task<IActionResult> Edit(int id)
        {
            var dto = await _productService.GetProductDetail(id);
            var viewModel = _mapper.Map<ProductUpdateViewModel>(dto);
            return View(viewModel);
        }

        [Authorize]
        [HttpPost("/product/edit/{id}")]
        public async Task<IActionResult> Edit(int id, ProductCreateViewModel product)
        {
            var dto = _mapper.Map<ProductUpdateDTO>(product);
            await _productService.Update(product.Id, dto);
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
