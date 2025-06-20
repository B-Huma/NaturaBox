using App.Data.Data.Entities;
using App.Data.Repositories;
using App.DTO.DTOs;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace App.DataApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IOrderItemRepository _orderItemRepo;
        private readonly ICartRepository _cartRepo;
        private readonly IOrderRepository _repo;
        private readonly IMapper _mapper;

        public OrderController(IOrderRepository repo, ICartRepository cartRepository, IOrderItemRepository orderItem, IMapper mapper)
        {
            _orderItemRepo = orderItem;
            _cartRepo = cartRepository;
            _repo = repo;
            _mapper = mapper;
        }
        [HttpPost]
        public async Task<IActionResult> CreateOrder(OrderDTO dto)
        {
            var userIdClaim = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userIdClaim == null || !int.TryParse(userIdClaim, out var userId))
            {
                return Unauthorized();
            }
            var cartItems = await _cartRepo.GetCartDetails(userId);
            if (cartItems == null)
            {
                return NotFound();
            }
            var order = new OrderEntity
            {
                UserId = userId,
                Address = dto.Address,
                OrderCode = Guid.NewGuid().ToString().Substring(0, 8).ToUpper(),
                CreatedAt = dto.CreatedAt,
                Items = new List<OrderItemEntity>()
            };
            await _repo.CreateOrderAsync(order);
            var orderItems = cartItems.Select(ci => new OrderItemEntity
            {
                OrderId = order.Id,
                ProductId = ci.ProductId,
                Quantity = ci.Quantity,
                UnitPrice = ci.Product.Price
            });
            await _orderItemRepo.AddRangeAsync(orderItems);
            _cartRepo.RemoveRange(cartItems);
            return Ok(new OrderResultDTO
            {
                OrderCode = order.OrderCode
            });
        }
        [HttpGet]
        public async Task<IActionResult> OrderDetails()
        {
            var userIdClaim = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userIdClaim == null || !int.TryParse(userIdClaim, out var userId))
            {
                return Unauthorized();
            }
            var orders = await _repo.OrderDetailsAsync(userId);
            var orderDto = orders.Select(order => new OrderDTO
            {
                Id = order.Id,
                OrderCode = order.OrderCode,
                Address = order.Address,
                CreatedAt = order.CreatedAt,
                Items = order.Items.Select(item => new OrderItemDTO
                {
                    ProductName = item.Product.Name,
                    ImageUrl = item.Product.Images.FirstOrDefault().Url,
                    Quantity = item.Quantity,
                    UnitPrice = item.Product.Price
                }).ToList()
            }).ToList();
            return Ok(orderDto);
        }
    }
}
