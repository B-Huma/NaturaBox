using App.Business.Abstract;
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
        private readonly ICartItemService _cartItem;
        private readonly IMapper _mapper;
        private readonly IOrderService _order;

        public OrderController(IOrderService order, IMapper mapper, ICartItemService cartItem)
        {
            _cartItem = cartItem;
            _mapper = mapper;
            _order = order;
        }
        [HttpGet]
        public async Task<IActionResult> Create()
        {
            var cartItems = await _cartItem.CartDetails();
            var viewModel = _mapper.Map<List<CartItemViewModel>>(cartItems);
            ViewBag.CartItems = viewModel;
            return View(new OrderCreateViewModel());
        }
        [HttpPost]
        public async Task<IActionResult> Create(OrderCreateViewModel model)
        {
            var userIdStr = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (string.IsNullOrEmpty(userIdStr)) return RedirectToAction("Login", "Auth");
            var userId = int.Parse(userIdStr);
            var dto = new OrderCreateDTO
            {
                UserId = userId,
                Address = model.Address,
                FullName = model.FullName
            };
           var orderCode = await _order.CreateOrder(dto);
            return RedirectToAction("Success", new { orderCode });
        }

        public async Task<IActionResult> Details()
        {
            var dtoList = await _order.OrderDetails();
            var viewModel = _mapper.Map<List<OrderViewModel>>(dtoList);
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
