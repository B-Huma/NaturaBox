using AdminMvc.Models.ViewModels;
using App.Business.Services;
using App.Data.Data;
using App.DTO.DTOs;
using AutoMapper;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace AdminMvc.Controllers
{
    public class AuthController : Controller
    {
        private readonly IMapper _mapper;
        private readonly AuthService _auth;
        public AuthController(AuthService auth, IMapper mapper)
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
            var token = await _auth.LoginRequestAsync(new LoginRequestDTO
            {
                Email = loginModel.Email,
                Password = loginModel.Password,
            });
            if (token != null)
            {
                // 1. Create claims
                var claims = new List<Claim>
        {
            new Claim(ClaimTypes.Name, loginModel.Email),
            // Add more claims as needed, e.g. roles, user id, etc.
        };

                // 2. Create identity and principal
                var identity = new ClaimsIdentity(claims, "access-token");
                var principal = new ClaimsPrincipal(identity);

                // 3. Sign in with the same scheme name as in Program.cs
                await HttpContext.SignInAsync("access-token", principal);

                return RedirectToAction("Index", "Home");
            }
            else
            {
                ModelState.AddModelError("", "Invalid login attempt.");
                return View(loginModel);
            }
        }

        [Route("/logout")]
        [HttpGet]
        public async Task<IActionResult> Logout()
        {
            await _auth.Logout();
            return RedirectToAction("Index", "Home");
        }
    }
}
