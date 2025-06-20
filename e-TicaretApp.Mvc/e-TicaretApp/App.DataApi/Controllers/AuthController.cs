using App.Data.Data.Entities;
using App.Data.Repositories;
using App.DTO.DTOs;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace App.DataApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class AuthController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IConfiguration _config;
        private readonly IUserRepository _user;

        public AuthController(IUserRepository user, IConfiguration config, IMapper mapper)
        {
            _mapper = mapper;
            _config = config;
            _user = user;
        }
        [AllowAnonymous]
        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromBody] LoginRequestDTO request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var user = await _user.GetByEmail(request.Email);
            if (user == null || user.Password != request.Password)
            {
                return NotFound("Invalid Credentials");
            }

            var token = GenerateJwtToken(user);

            var response = new LoginResponseDTO
            {
                Token = token,
                ExpiresAt = DateTime.UtcNow.AddHours(1),
                User = _mapper.Map<UserDTO>(user)
            };

            return Ok(response);
        }



        [AllowAnonymous]
        [HttpPost("Register")]
        public async Task<IActionResult> Register(RegisterRequestDTO dto)
        {
            if (await _user.EmailExists(dto.Email))
            {
                return BadRequest();
            }
            var user = _mapper.Map<UserEntity>(dto);
            await _user.CreateUser(user);
            return Ok();
        }
        [HttpPost("Logout")]
        public async Task<IActionResult> Logout()
        {
            Response.Cookies.Delete("access-token");
            return Ok();
        }
        [HttpPut("RenewPassword")]
        public async Task<IActionResult> RenewPassword(ChangePasswordDTO dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            var userId =int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
            var user = await _user.GetById(userId);
            if (user is null)
            {
                ModelState.AddModelError("", "User not found.");
            }
            if (user.Password != dto.CurrentPassword)
            {
                ModelState.AddModelError("", "Current password is not correct");
            }
            if (dto.NewPassword != dto.ConfirmPassword)
            {
                ModelState.AddModelError("", "Password do not match.");
            }
            if (dto.NewPassword.Length < 6)
            {
                ModelState.AddModelError("", "Password must be at least 6 characters");
            }
            user.Password = dto.NewPassword;
            _user.UpdateUser(user);
            return Ok(user);
        }
        [NonAction]
        public string GenerateJwtToken(UserEntity user)
        {
            var claims = new List<Claim>()
            {
                new Claim(ClaimTypes.Name, $"{user.FirstName}{user.LastName}"),
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.Role, user.RoleId.ToString()),
                new Claim("jti", Guid.NewGuid().ToString())
            };
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Secret"]));
            var token = new JwtSecurityToken(
                issuer: "NaturaBox",
                audience: "Api",
                claims: claims,
                expires: DateTime.UtcNow.AddHours(1),
                signingCredentials: new SigningCredentials(key, SecurityAlgorithms.HmacSha256)
            );
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
