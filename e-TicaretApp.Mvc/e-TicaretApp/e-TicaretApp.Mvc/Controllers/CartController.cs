﻿using App.Business.Abstract;
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
        private readonly ICartItemService _service;

        public CartController(ICartItemService service, IMapper mapper)
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

        [HttpPost]
        public async Task<IActionResult> AddProduct(int productId, byte quantity)
        {
            var userIdStr = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (string.IsNullOrEmpty(userIdStr)) return RedirectToAction("Login", "Auth");
            var userId = int.Parse(userIdStr);
            var dto = new CartItemDTO
            {
                ProductId = productId,
                Quantity = quantity,
                UserId = userId
            };
            await _service.AddtoCart(dto);
            return RedirectToAction("CartDetails");
        }
        [HttpPost]
        public async Task<IActionResult> Edit(int productId, byte quantity)
        {
            var userIdStr = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (string.IsNullOrEmpty(userIdStr)) return RedirectToAction("Login", "Auth");
            var userId = int.Parse(userIdStr);
            var dto = new UpdateCartItemDTO
            {
                ProductId = productId,
                Quantity = quantity,
                UserId = userId
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
