using AdminMvc.Models.ViewModels;
using App.Business.Services;
using App.Data.Data;
using App.DTO.DTOs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace AdminMvc.Controllers
{
    public class UserController : Controller
    {
        private readonly UserService _userService;

        public UserController(UserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        public async Task<IActionResult> List()
        {
            var users = await _userService.GetNonAdminUsers();
            var viewModel = users.Select(dto => new UserListViewModel
            {
                Id = dto.Id,
                FirstName = dto.FirstName,
                LastName = dto.LastName,
                Email = dto.Email,
                Role =  dto.Role,
                Enabled = dto.Enabled
            }).ToList();
            return View(users);
        }
        [HttpGet]
        public async Task<IActionResult> UnApprovedUser()
        {
            var users = await _userService.GetUnapprovedUsers();
            var viewModel = users.Select(dto => new UserListViewModel
            {
                Id = dto.Id,
                FirstName = dto.FirstName,
                LastName = dto.LastName,
                Email = dto.Email,
                Role = dto.Role,
                Enabled = dto.Enabled
            }).ToList();
            return View(users);
        }

        [HttpGet]
        public async Task<IActionResult> Approve(int id)
        {
            await _userService.ApproveUser(id);
            return RedirectToAction("List");
        }
        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            await _userService.DeleteRequest(id);
            
            return RedirectToAction("List");
        }



    }
}
