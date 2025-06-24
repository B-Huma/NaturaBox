using App.Data.Repositories;
using App.DTO.DTOs;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace App.DataApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IAdminUserRepository _repo;

        public UserController(IAdminUserRepository repo, IMapper mapper)
        {
            _mapper = mapper;
            _repo = repo;
        }

        [HttpGet]
        [Route("users")]
        public async Task<IActionResult> GetAll()
        {
            var users = await _repo.GetUsersWithoutAdmin();
            var userDTO =_mapper.Map<List<AdminUserDTO>>(users);
            return Ok(userDTO);
        }
        [HttpGet]
        [Route("UnapprovedUsers")]
        public async Task<IActionResult> GetUnapproved()
        {
            var users = await _repo.UnApprovedUsers();
            var userDTO = _mapper.Map<List<AdminUserDTO>>(users);
            return Ok(userDTO);
        }
        [HttpPut("Update/{id}")]
        public async Task<IActionResult> Update(int id)
        {
            var user = await _repo.GetByIdAsync(id);

            if (user == null) return NotFound("Could not find a user");

            user.Enabled = true;

            await _repo.UpdateAsync(user);
            return Ok();
        }
        [HttpDelete("Delete/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var user = await _repo.GetByIdAsync(id);

            if (user == null) return NotFound("Could not find a user");

            user.Enabled = false;
            await _repo.UpdateAsync(user);
            return NoContent();
        }
    }
}
