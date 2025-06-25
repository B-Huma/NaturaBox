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
    public class CartItemController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IUserRepository _user;
        private readonly IProductRepository _product;
        private readonly ICartRepository _repo;
        public CartItemController(ICartRepository repo, IUserRepository user, IProductRepository product, IMapper mapper)
        {
            _mapper = mapper;
            _user = user;
            _product = product;
            _repo = repo;
        }
        [HttpGet("CartDetails")]
        public async Task<ActionResult<List<CartItemDTO>>> Get()
        {
            var userIdStr = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (string.IsNullOrEmpty(userIdStr))
                return Ok(new List<CartItemDTO>());

            var userId = int.Parse(userIdStr);
            var user = await _user.GetById(userId);

            if (user == null)
                return Ok(new List<CartItemDTO>());

            var cartItems = await _repo.GetCartDetails(userId);

            if (cartItems == null || !cartItems.Any())
                return Ok(new List<CartItemDTO>());

            var cartItemDTO = _mapper.Map<List<CartItemDTO>>(cartItems);
            return Ok(cartItemDTO);
        }
        [HttpPost("Add")]
        public async Task<IActionResult> Add(CartItemDTO cartItem)
        {
            if (!User.Identity.IsAuthenticated)
            {
                return Unauthorized();
            }
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            var userIdClaim = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (string.IsNullOrEmpty(userIdClaim) || !int.TryParse(userIdClaim, out int userId))
            {
                return Unauthorized();
            }
            var user = await _user.GetById(cartItem.UserId);
            if (user == null) return NotFound("Could not find user");

            var product = await _product.GetById(cartItem.ProductId);
            if (product == null) return NotFound("Could not find product");

            var existingItems = await _repo.GetCartDetails(cartItem.UserId);
            var existingItem = existingItems.FirstOrDefault(x => x.ProductId == cartItem.ProductId);

            if (existingItem != null)
            {
                existingItem.Quantity += cartItem.Quantity;
                await _repo.EditCartItemsQuantity(existingItem);
                var updatedDTO = new CartItemDTO
                {
                    UserId = existingItem.UserId,
                    ProductId = existingItem.ProductId,
                    Quantity = existingItem.Quantity
                };
                return Ok(updatedDTO);
            }
            else
            {
                var newCartItem = new CartItemEntity
                {
                    UserId = cartItem.UserId,
                    ProductId = cartItem.ProductId,
                    Quantity = cartItem.Quantity
                };
                await _repo.AddProductToCart(newCartItem);
                return Ok(cartItem);
            }
        }
        [HttpPut]
        public async Task<IActionResult> Update(UpdateCartItemDTO dto)
        {
            if (!User.Identity.IsAuthenticated)
            {
                return Unauthorized();
            }
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            var userIdClaim = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (string.IsNullOrEmpty(userIdClaim) || !int.TryParse(userIdClaim, out int userId))
            {
                return Unauthorized();
            }
            var existingItem = (await _repo.GetCartDetails(dto.UserId))
                .FirstOrDefault(x => x.ProductId == dto.ProductId);
            if (existingItem == null)
            {
                return NotFound("Could not find item");
            }
            existingItem.Quantity = dto.Quantity;
            await _repo.EditCartItemsQuantity(existingItem);
            return Ok();
        }
        [HttpDelete("{productId}")]
        public async Task<IActionResult> Delete(int productId)
        {
            if (!User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Login", "Auth");
            }

            var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
            var existingItem = (await _repo.GetCartDetails(userId))
                .FirstOrDefault(x => x.ProductId == productId);
            if (existingItem == null) return NotFound("Item not found in cart");
            await _repo.DeleteProductFromCart(existingItem.Id);
            return NoContent();
        }
    }
}
