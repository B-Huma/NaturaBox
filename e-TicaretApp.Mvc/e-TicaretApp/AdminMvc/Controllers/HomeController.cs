using System.Diagnostics;
using AdminMvc.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AdminMvc.Controllers
{
    [Authorize(AuthenticationSchemes = "MyCookieAuth")]
    public class HomeController : Controller
    {
        [Authorize(Roles ="admin")]
        public IActionResult Index()
        {
            return View();
        }

        

        
    }
}
