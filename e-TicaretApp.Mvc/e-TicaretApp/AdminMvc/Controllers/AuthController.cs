using AdminMvc.Models.ViewModels;
using App.Business.Abstract;
using App.Data.Data;
using App.DTO.DTOs;
using AutoMapper;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace AdminMvc.Controllers
{
    public class AuthController : Controller
    {
        private readonly IMapper _mapper;
        private readonly IAuthService _auth;
        public AuthController(IAuthService auth, IMapper mapper)
        {
            _mapper = mapper;
            _auth = auth;
        }

        [HttpGet]
        public async Task<IActionResult> Login()
        {            
            return View();
        }

        
        [HttpPost]
        public async Task<IActionResult> Login([FromForm] LoginViewModel loginModel)
        {
            var dto = _mapper.Map<LoginRequestDTO>(loginModel);
            var loginResponse = await _auth.LoginRequestAsync(dto);

            if (loginResponse == null || string.IsNullOrEmpty(loginResponse.Token))
            {
                ModelState.AddModelError("", "Invalid Credentials");
                return View(loginModel);
            }

            // JWT token'dan claims'leri çıkar ve ClaimsPrincipal oluştur
            var handler = new JwtSecurityTokenHandler();
            var jwtToken = handler.ReadJwtToken(loginResponse.Token);

            var claims = jwtToken.Claims;
            var identity = new ClaimsIdentity(claims, "access-token");
            var principal = new ClaimsPrincipal(identity);

            // Kullanıcıyı giriş yaptır
            await HttpContext.SignInAsync("access-token", principal);

            Response.Cookies.Append("access-token", loginResponse.Token, new CookieOptions
            {
                HttpOnly = true,
                Secure = true,
                SameSite = SameSiteMode.Strict,
                Expires = loginResponse.ExpiresAt
            });

            return RedirectToAction("Index", "Home");
        }
        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync("access-token");
            await _auth.Logout();
            return RedirectToAction("Index", "Home");
        }
        [HttpGet]
        public async Task<IActionResult> AccessDenied()
        {
            return View();
        }
    }
}
