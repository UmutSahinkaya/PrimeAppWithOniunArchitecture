using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using PrimeApp.Domain.Entities;
using PrimeApp.Infrastructure.Identity;
using PrimeApp.MVC.Models;
using System.Diagnostics;
using System.Security.Claims;

namespace PrimeApp.MVC.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly UserManager<ApplicationUser> _userManager;


        public HomeController(ILogger<HomeController> logger, UserManager<ApplicationUser> userManager)
        {
            _logger = logger;
            _userManager = userManager;
        }

        public IActionResult Index()
        {
            ViewBag.CurrentUser = User.FindFirstValue(ClaimTypes.Name);
            return View();
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
