using App.Data.Data.Entities;
using App.Data.Repositories;
using App.DTO;
using App.DTO.DTOs;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace App.DataApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IAdminCategoryRepository _repository;

        public CategoryController(IAdminCategoryRepository repository, IMapper mapper)
        {
            _mapper = mapper;
            _repository = repository;
        }

        [HttpGet("categoryList")]
        public async Task<IActionResult> Get()
        {
            var categories = await _repository.GetCategoriesAsync();
            if (categories == null || !categories.Any())
            {
                return NotFound();
            }
            var categoryDTO = _mapper.Map<List<CategoryDTO>>(categories);

            return Ok(categoryDTO);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var category = await _repository.GetByIdAsync(id);
            if (category == null)
            {
                return NotFound();
            }
            var categoryDTO = _mapper.Map<CategoryDTO>(category);
            return Ok(categoryDTO);
        }
        [HttpPost("Create")]
        public async Task<IActionResult> Create(CategoryDTO categoryDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            var newCategory = _mapper.Map<CategoryEntity>(categoryDTO);
            await _repository.AddCategoryAsync(newCategory);
            return CreatedAtAction(nameof(GetById), new {id = newCategory.Id}, categoryDTO);
        }
        [HttpPut("Update/{id}")]
        public async Task<IActionResult> Update(int id, CategoryDTO categoryDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var category = await _repository.GetByIdAsync(id);
            if (category == null)
            {
                return NotFound();
            }
            var updatedCategory = _mapper.Map(categoryDTO, category);
            await _repository.UpdateAsync(updatedCategory);
            return NoContent();
        }
        [HttpDelete("Delete/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _repository.DeleteCategoryAsync(id);
            return NoContent();
        }
    }
}
