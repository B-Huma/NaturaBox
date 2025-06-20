using App.Business.Services;
using App.Data.Data;
using App.Data.Data.Entities;
using App.DTO.DTOs;
using AutoMapper;
using e_TicaretApp.Mvc.Models;
using e_TicaretApp.Mvc.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace e_TicaretApp.Mvc.Controllers
{
    public class HomeController : Controller
    {
        private readonly IMapper _mapper;
        private readonly ProductService _productService;

        public HomeController(ProductService productService, IMapper mapper)
        {
            _mapper = mapper;
            _productService = productService;
        }
        public IActionResult Index()
        {
            return View();
        }

        [Route("about-us")]
        public IActionResult AboutUs()
        {
            return View();
        }

        public IActionResult Contact()
        {
            return View();
        }

        public async Task<IActionResult> Listing()
        {
            var dto = await _productService.Get();
            var viewModel = _mapper.Map<ShopViewModel>(dto);
            return View(viewModel);            
        }

        public async Task<IActionResult> ProductDetail(int id)
        {
            var dto = _productService.GetProductDetail(id);
            var viewModel = _mapper.Map<ProductDetailViewModel>(dto);
            return View(viewModel);
        }

        public IActionResult News()
        {
            return View();
        }

        public IActionResult SingleNew()
        {
            return View();
        }
    }
}
