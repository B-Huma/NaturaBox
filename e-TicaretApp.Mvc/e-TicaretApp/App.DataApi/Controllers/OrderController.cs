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
        [HttpPost("CreateOrder")]
        public async Task<IActionResult> CreateOrder(OrderCreateDTO dto)
        {
            try
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
                    CreatedAt = DateTime.Now,
                    Items = new List<OrderItemEntity>()
                };
                
                Console.WriteLine($"Order oluşturuluyor...");
                await _repo.CreateOrderAsync(order);
                Console.WriteLine($"Order Id: {order.Id}");
                
                var orderItems = cartItems.Select(ci => new OrderItemEntity
                {
                    OrderId = order.Id,
                    ProductId = ci.ProductId,
                    Quantity = ci.Quantity,
                    UnitPrice = ci.Product.Price
                }).ToList();
                
                Console.WriteLine($"OrderItems Count: {orderItems.Count}");
                Console.WriteLine($"First OrderId: {orderItems.First().OrderId}");
                
                await _orderItemRepo.AddRangeAsync(orderItems);
                Console.WriteLine($"OrderItems kaydedildi");
                
                await _cartRepo.RemoveRange(cartItems);
                
                return Ok(new OrderResultDTO
                {
                    OrderCode = order.OrderCode
                });
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Hata: {ex.Message}");
                Console.WriteLine($"StackTrace: {ex.StackTrace}");
                return BadRequest($"Hata: {ex.Message}");
            }
        }
        [HttpGet("OrderDetails")]
        public async Task<IActionResult> OrderDetails()
        {
            var userIdClaim = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userIdClaim == null || !int.TryParse(userIdClaim, out var userId))
            {
                return Unauthorized();
            }
            var orders = await _repo.OrderDetailsAsync(userId);
            Console.WriteLine($"Orders Count: {orders.Count()}");
            foreach (var order in orders)
            {
                Console.WriteLine($"Order {order.Id} - Items Count: {order.Items?.Count ?? 0}");
                if (order.Items != null)
                {
                    foreach (var item in order.Items)
                    {
                        Console.WriteLine($"  Item: ProductId={item.ProductId}, Product={item.Product?.Name}, Quantity={item.Quantity}");
                    }
                }
            }
            var orderDto = orders.Select(order => new OrderDTO
            {
                Id = order.Id,
                OrderCode = order.OrderCode,
                Address = order.Address,
                CreatedAt = order.CreatedAt,
                Items = order.Items.Select(item => new OrderItemDTO
                {
                    ProductName = item.Product?.Name ?? "Ürün Adı Yok",
                    ImageUrl = item.Product?.Images?.FirstOrDefault()?.Url ?? "",
                    Quantity = item.Quantity,
                    UnitPrice = item.Product?.Price ?? 0
                }).ToList()
            }).ToList();
            return Ok(orderDto);
        }
    }
}
