using AdminMvc.Models.ViewModels;
using App.Business.Services;
using App.Data.Data;
using App.Data.Data.Entities;
using App.DTO.DTOs;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AdminMvc.Controllers
{
    [Authorize]
    public class CategoryController : Controller
    {
        private readonly IMapper _mapper;
        private readonly CategoryService _service;

        public CategoryController(CategoryService service, IMapper mapper)
        {
            _mapper = mapper;
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> List()
        {
            var categories = await _service.GetCategories();
            var categoryViewModel = _mapper.Map<List<CategoryViewModel>>(categories);
            return View(categoryViewModel);
            
        }
        [HttpGet("/categories/create")]

        public async Task<IActionResult> Create()
        {
            var model = new CategoryViewModel();
            return View(model);
        }

        [HttpPost("/categories/create")]
        public async Task<IActionResult> Create([FromForm] CategoryViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var categoryDTO = _mapper.Map<CategoryDTO>(model);            

            await _service.CreateCategory(categoryDTO); 

            return RedirectToAction("List", "Product");
        }

        [Route("/edit/{id}")]
        [HttpGet]
        public async Task<IActionResult> Edit([FromRoute]int id)
        {
            
            var category = await _service.GetCategoryById(id);
            if (category == null) return View();
            var existingCategory = _mapper.Map<CategoryViewModel>(category);
            return View(existingCategory);
        }
        [Route("/edit/{id}")]
        [HttpPost]
        public async Task<IActionResult> Edit([FromRoute] int id, [FromForm] CategoryViewModel model)
        {
            if (id != model.Id)
            {
                return BadRequest();
            }
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("", "Invalid entries");
                return View(model);
            }
            var dto = _mapper.Map<CategoryDTO>(model);
            await _service.EditCategory(id,dto);
            return RedirectToAction("List","Category");
        }

        [Route("/categories/delete/{id}")]
        [HttpGet]
        public async Task<IActionResult> Delete([FromRoute]int id)
        {
            await _service.GetCategoryById(id);
            return RedirectToAction("List", "Category");
        }
    }
}
