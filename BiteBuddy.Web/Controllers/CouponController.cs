using BiteBuddy.Web.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace BiteBuddy.Web.Controllers
{
    public class CouponController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public CouponController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View(); //Test
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}