using Book2.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace Book2.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View("LandingPage"); // Nó sẽ tìm đến Views/Home/Index.cshtml
        }
    }
}
