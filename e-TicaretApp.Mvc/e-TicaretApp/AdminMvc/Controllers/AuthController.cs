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
            return RedirectToAction("Index", "Home");
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
