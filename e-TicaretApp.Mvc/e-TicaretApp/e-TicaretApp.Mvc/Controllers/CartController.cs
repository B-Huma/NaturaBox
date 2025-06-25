using App.Business.Services;
using App.Data.Data;
using App.Data.Data.Entities;
using App.DTO.DTOs;
using AutoMapper;
using e_TicaretApp.Mvc.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace e_TicaretApp.Mvc.Controllers
{
    public class CartController : Controller
    {
        private readonly IMapper _mapper;
        private readonly CartItemService _service;

        public CartController(CartItemService service, IMapper mapper)
        {
            _mapper = mapper;
            _service = service;
        }
        [HttpGet]
        public async Task<IActionResult> CartDetails()
        {            
            var cartItems = await _service.CartDetails();
            var viewModel = _mapper.Map<List<CartItemViewModel>>(cartItems);
            return View(viewModel);
        }

        [HttpGet]
        public async Task<IActionResult> AddProduct(int productId, byte quantity)
        {               
            var dto = new CartItemDTO
            {
                ProductId = productId,
                Quantity = quantity
            };
            await _service.AddtoCart(dto);
            return RedirectToAction("CartDetails");
        }
        [HttpPost]
        public async Task<IActionResult> Edit(int productId, byte quantity)
        {
            var dto = new UpdateCartItemDTO
            {
                ProductId = productId,
                Quantity = quantity
            };
            await _service.UpdateCartQuantity(dto);            
            return RedirectToAction("CartDetails");
        }
        [HttpPost]
        public async Task<IActionResult> Delete(int productId)
        {
            await _service.DeleteCartItem(productId);            
            return RedirectToAction("CartDetails");
        }

    }

}
