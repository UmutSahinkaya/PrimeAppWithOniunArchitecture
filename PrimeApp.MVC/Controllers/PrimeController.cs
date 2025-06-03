using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PrimeApp.Domain.Common;
using PrimeApp.Domain.Entities;
using PrimeApp.Infrastructure.Identity;
using PrimeApp.Infrastructure.Persistence;
using System.Security.Claims;

namespace PrimeApp.MVC.Controllers
{
    [Authorize]
    public class PrimeController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly UserManager<ApplicationUser> _userManager;

        public PrimeController(IUnitOfWork unitOfWork, UserManager<ApplicationUser> userManager)
        {
            _unitOfWork = unitOfWork;
            _userManager = userManager;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Index(string inputNumbers)
        {
            var userId = User.FindFirstValue("uid");

            if (!Guid.TryParse(userId, out var parsedUserId))
                return Unauthorized("Kullanıcı ID geçersiz.");

            var userExists = await _userManager.FindByIdAsync(parsedUserId.ToString());
            if (userExists == null)
                return Unauthorized("Kullanıcı bulunamadı.");



            var numbers = inputNumbers
                            .Split(',')
                            .Select(s => int.TryParse(s.Trim(), out var n) ? n : 0)
                            .Where(n => n > 1)
                            .ToList();

            int? maxPrime = numbers.Where(IsPrime).OrderByDescending(n => n).FirstOrDefault();

            var entity = new PrimeInput
            {
                UserId = parsedUserId,
                InputNumbers = inputNumbers,
                MaxPrime = maxPrime,
                CreatedAt = DateTime.UtcNow
            };

            await _unitOfWork.Repository<PrimeInput>().AddAsync(entity);
            await _unitOfWork.SaveChangesAsync();


            ViewBag.Result = maxPrime;
            return View();
        }

        private bool IsPrime(int number)
        {
            if (number < 2) return false;
            for (int i = 2; i <= Math.Sqrt(number); i++)
                if (number % i == 0) return false;
            return true;
        }
    }
}
