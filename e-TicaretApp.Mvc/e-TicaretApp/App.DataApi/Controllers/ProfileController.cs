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
    public class ProfileController : ControllerBase
    {
        private readonly IProductRepository _product;
        private readonly IMapper _mapper;
        private readonly IUserRepository _user;

        public ProfileController(IUserRepository user, IMapper mapper, IProductRepository product)
        {
            _product = product;
            _mapper = mapper;
            _user = user;
        }
        [HttpGet("profile-details")]
        public async Task<IActionResult> ProfileDetails()
        {
            var userId= int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
            var user = await _user.ExistingUser(userId);
            if (user == null) return NotFound();
            var dto =_mapper.Map<ExistingUserDTO>(user);
            return Ok(dto);
        }
        [HttpPut("EditProfile/{id}")]
        public async Task<IActionResult> EditProfile(int id,UserEditDTO dto)
        {
            var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
            if (userId != id) return Forbid();

            if(!ModelState.IsValid) return BadRequest(ModelState);

            var user = await _user.UserIdExists(userId);
            if (user == null) return NotFound();

            _mapper.Map(dto, user);
            await _user.UpdateUser(user);            
            return Ok(new { message = "Profile updated successfully." });
        }
        [HttpGet("user-products")]
        public async Task<IActionResult> UsersProducts()
        {
            var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
            var products = await _product.GetSellersProducts(userId);

            var dtos = _mapper.Map<List<ProductViewDTO>>(products);
            return Ok(dtos);
        }
        [HttpPost("request-seller")]
        public async Task<IActionResult> RequestSeller()
        {
            var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
            var user = await _user.UserIdExists(userId);

            if (user == null)
                return NotFound(new { message = "User not found" });

            if (!user.Enabled)
            {
                user.Enabled = true;
                await _user.UpdateUser(user);
                return Ok(new { message = "Your seller request has been submitted." });
            }

            return BadRequest(new { message = "You already have a pending seller request." });
        }
    }
}
