using App.DTO.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Business.Abstract
{
    public interface IProductService
    {
        Task<ShopDTO> Get();
        Task<ProductDetailDTO> GetProductDetail(int id);
        Task Create(ProductCreateDTO dto);
        Task Update(int id, ProductUpdateDTO dto);
        Task DeleteProduct(int id);
    }
}
