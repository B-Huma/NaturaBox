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
    public class OrderController : Controller
    {
        private readonly CartItemService _cartItem;
        private readonly IMapper _mapper;
        private readonly OrderService _order;

        public OrderController(OrderService order, IMapper mapper, CartItemService cartItem)
        {
            _cartItem = cartItem;
            _mapper = mapper;
            _order = order;
        }
        [HttpGet]
        public async Task<IActionResult> Create()
        {
            var cartItems = _cartItem.CartDetails;
            var viewModel = _mapper.Map<CartItemViewModel>(cartItems);
            ViewBag.CartItems = viewModel;
            return View(new OrderCreateViewModel());
        }
        [HttpPost]
        public async Task<IActionResult> Create(OrderCreateViewModel model)
        {
            var dto = _mapper.Map<OrderDTO>(model);
           var orderCode = await _order.CreateOrder(dto);
            return RedirectToAction("Success", new { orderCode });
        }

        public async Task<IActionResult> Details()
        {
            var viewModel = await _order.OrderDetails();
            return View(viewModel);
        }

        public async Task<IActionResult> Success(string orderCode)
        {
            if (string.IsNullOrEmpty(orderCode))
            {
                return RedirectToAction("Index", "Home");
            }
            ViewBag.OrderCode = orderCode;
            return View();
        }


    }
}
