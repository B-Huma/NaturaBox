using App.Business.Services;
using App.Data.Data;
using App.Data.Data.Entities;
using App.DTO.DTOs;
using AutoMapper;
using e_TicaretApp.Mvc.Models.ViewModels;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace e_TicaretApp.Mvc.Controllers
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
        [Route("/register")]
        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }
        [Route("/register")]
        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel user)
        {
            var dto = _mapper.Map<RegisterRequestDTO>(user);
            await _auth.RegisterAsync(dto);
            return RedirectToAction("Login");
        }
        
        [Route("/login")]
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [Route("/login")]
        [HttpPost]
        public async Task<IActionResult> Login([FromForm] LoginViewModel loginModel)
        {
            if (!ModelState.IsValid)
            {
                return View(loginModel);
            }

            var loginResponse = await _auth.LoginRequestAsync(new LoginRequestDTO
            {
                Email = loginModel.Email,
                Password = loginModel.Password,
            });

            if (loginResponse == null || string.IsNullOrEmpty(loginResponse.Token))
            {
                ModelState.AddModelError("", "Geçersiz e-posta veya şifre.");
                return View(loginModel);
            }

            Response.Cookies.Append("access-token", loginResponse.Token, new CookieOptions
            {
                HttpOnly = true,
                Secure = true,
                SameSite = SameSiteMode.Strict,
                Expires = loginResponse.ExpiresAt
            });

            return RedirectToAction("Index", "Home");
        }



        [Route("/forgot-password")]
        [HttpGet]
        public IActionResult ForgotPassword()
        {
            return View();
        }

        [Route("/forgot-password")]
        [HttpPost]
        public IActionResult ForgotPassword([FromForm] object forgotPasswordModel)
        {
            return View();
        }
        [Authorize]
        [Route("/renew-password/")]
        [HttpGet]
        public async Task<IActionResult> RenewPassword()
        {
            return View(new ChangePasswordViewModel());
        }
        [Authorize]
        [Route("/renew-password")]
        [HttpPost]
        public async Task<IActionResult> RenewPassword(ChangePasswordViewModel model)
        {
            var dto = _mapper.Map<ChangePasswordDTO>(model);
            await _auth.ChangePassword(dto);
            return RedirectToAction("Details", "Profile", new { id = model.UserId });
        }
        [Authorize]
        [Route("/logout")]
        public async Task<IActionResult> Logout()
        {
            await _auth.Logout();
            return RedirectToAction("Index", "Home");
        }



    }
}
