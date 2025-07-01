using System.Security.Claims;
using System.Threading.Tasks;
using App.Business.Abstract;
using App.Data.Data;
using App.Data.Data.Entities;
using App.DTO.DTOs;
using AutoMapper;
using e_TicaretApp.Mvc.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;


namespace e_TicaretApp.Mvc.Controllers
{
    public class ProfileController : Controller
    {
        private readonly IMapper _mapper;
        private readonly IProfileService _profile;

        public ProfileController(IProfileService profile, IMapper mapper)
        {
            _mapper = mapper;
            _profile = profile;
        }

        [Authorize]
        public async Task<IActionResult> Details()
        {
            var user = await _profile.GetProfileDetails();
            var viewModel = _mapper.Map<UserViewModel>(user);            
            return View(viewModel);

        }
        [Authorize]
        [Route("user/edit/{id}")]
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var userDto= await _profile.GetProfileDetails();
            var viewModel = _mapper.Map<EditViewModel>(userDto);
            return View(viewModel);
        }
        [Authorize]
        [Route("user/edit/{id}")]
        [HttpPost]
        public async Task<IActionResult> Edit(EditViewModel model)
        {
            if (!ModelState.IsValid) return View(model);

            var dto = _mapper.Map<UserEditDTO>(model);
            var success = await _profile.UpdateProfile(model.Id, dto);
            if (!success)
            {
                TempData["Error"] = "Profile update failed";
                return View(model);
            }
            TempData["Message"] = "Profile updated successfully";

            return RedirectToAction("Details", "Profile");
        }
        [Authorize]
        public IActionResult MyOrders()
        {
            return View();
        }
        public async Task<IActionResult> MyProducts()
        {
            var dtoList = await _profile.GetUserProductsAsync();

            var viewModel = new MyProductsViewModel
            {
                Products = _mapper.Map<List<ProductTableItem>>(dtoList)
            };

            return View(viewModel);
        }
        [Authorize]
        [HttpPost]
        public async Task<IActionResult> RequestSeller()
        {
            var message = await _profile.RequestSeller();
            TempData["Message"] = message;
            return RedirectToAction("Details", "Profile");
        }
    }
}
