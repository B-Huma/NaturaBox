using AdminMvc.Models.ViewModels;
using App.Business.Abstract;
using App.Data.Data;
using App.Data.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AdminMvc.Controllers
{
    public class CommentController : Controller
    {
        private readonly IProductCommentService _service;

        public CommentController(IProductCommentService service)
        {
            _service=service;
        }

        [HttpGet]
        public async Task<IActionResult> List()
        {
            var comments = await _service.ProductComments();
            var viewModel = comments.Select(x=> new CommentViewModel
            {
                ProductName = x.ProductName,
                UserFullName = x.UserFullName,
                Text = x.Text,
                StarCount = x.StarCount,
                IsConfirmed = x.IsConfirmed,
                CreatedAt = x.CreatedAt
            }).ToList();
            
            return View(viewModel);
        }

        public async Task<IActionResult> Comments()
        {
            var comments = await _service.UnapprovedComments();
            var viewModel = comments.Select(x => new CommentViewModel
            {
                ProductName = x.ProductName,
                UserFullName = x.UserFullName,
                Text = x.Text,
                StarCount = x.StarCount,
                IsConfirmed = x.IsConfirmed,
                CreatedAt = x.CreatedAt
            }).ToList();

            return View(viewModel);
        }

        [HttpGet]
        public async Task<IActionResult> Confirm(int id)
        {
            await _service.Approve(id);
            
            return RedirectToAction("Comments");
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            await _service.Delete(id);
            
            return RedirectToAction("Comments");
        }
    }
}
