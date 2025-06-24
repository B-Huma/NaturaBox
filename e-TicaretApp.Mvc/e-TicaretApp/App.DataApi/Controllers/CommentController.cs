using App.Data.Data.Entities;
using App.Data.Repositories;
using App.DTO.DTOs;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.Threading.Tasks;

namespace App.DataApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommentController : ControllerBase
    {
        private readonly IProductRepository _product;
        private readonly IAdminCommentRepository _repo;
        private readonly IMapper _mapper;

        public CommentController(IAdminCommentRepository repo, IProductRepository product, IMapper mapper)
        {
            _product = product;
            _repo = repo;
            _mapper = mapper;
        }

        [HttpGet("comment")]
        public async Task<IActionResult> Get()
        {
            var comments = await _repo.GetAllComments();
            if (comments == null)
            {
                throw new Exception("Could not find any comments");
            }
            var commentDTO = _mapper.Map<List<AdminProductCommentDTO>>(comments);
            return Ok(commentDTO);

        }
        [HttpGet("comment/unapproved")]
        public async Task<IActionResult> GetUnapproved()
        {
            var comments = await _repo.GetAllUnapprovedComments();
            if(comments == null)
            {
                return NotFound();
            }
            var commentDTO = _mapper.Map<List<AdminProductCommentDTO>>(comments);
            return Ok(commentDTO);
        }
        [HttpPut("ApproveComment/{id}")]
        public async Task ApproveComment(int id)
        {
            await _repo.ApproveComment(id);
        }
        [HttpDelete("Delete/{id}")]
        public async Task Delete(int id)
        {
            await _repo.DeleteComment(id);
        }
        [HttpPost("ProductComment/{productId}")]
        public async Task<IActionResult> ProductComment(int productId, SaveProductCommentDTO dto)
        {
            var userIdClaim = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userIdClaim == null || !int.TryParse(userIdClaim, out var userId))
            {
                return Unauthorized();
            }
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            if (!await _product.HasProduct(productId))
            {
                return NotFound();
            }
            if (await _repo.HasProductComment(productId, userId))
            {
                return BadRequest();
            }
            var product = await _product.GetProduct(productId);
            var comment = _mapper.Map<ProductCommentEntity>(dto);
            await _repo.AddAsync(comment);
            return Ok();
        }
    }
}
