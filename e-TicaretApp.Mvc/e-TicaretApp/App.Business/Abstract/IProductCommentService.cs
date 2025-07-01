using App.DTO.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Business.Abstract
{
    public interface IProductCommentService
    {
        Task<List<AdminProductCommentDTO>> ProductComments();
        Task<List<AdminProductCommentDTO>> UnapprovedComments();
        Task Approve(int id);
        Task Delete(int id);
        Task AddComment(int productId, SaveProductCommentDTO comment);
    }
}
