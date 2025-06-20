using AdminMvc.Models.ViewModels;
using App.Data.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace AdminMvc.Controllers
{
    public class ProductController : Controller
    {
        
        
        [HttpGet]
        public async Task<IActionResult> List()
        {
            return View();
        }

        [Route("/products/filter")]
        [HttpGet]
        public IActionResult Filter([FromQuery] object filterOptions)
        {
            // will return filtered products as json
            return Json(new { });
        }

        
        [HttpGet]
        public IActionResult Delete([FromRoute] int productId)
        {
            return View();
        }
    }
}
