using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PrimeApp.Domain.Common;
using PrimeApp.Domain.Entities;
using PrimeApp.Infrastructure.Identity;
using PrimeApp.MVC.Models;

namespace PrimeApp.MVC.Controllers
{
    //[Authorize(Roles = "Admin")]
    [Authorize]
    public class AdminController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly UserManager<ApplicationUser> _userManager;
        public AdminController(IUnitOfWork unitOfWork, UserManager<ApplicationUser> userManager)
        {
            _unitOfWork = unitOfWork;
            _userManager = userManager;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var inputs = await _unitOfWork.Repository<PrimeInput>()
            .GetAllAsync();

            var users = await _userManager.Users.ToListAsync();

            var list = inputs
                .Join(users,
                      input => input.UserId,
                      user => user.Id,
                      (input, user) => new PrimeInputViewModel
                      {
                          UserEmail = user.Email!,
                          InputNumbers = input.InputNumbers,
                          MaxPrime = input.MaxPrime,
                          CreatedAt = input.CreatedAt
                      })
                .OrderByDescending(x => x.CreatedAt)
                .ToList();

            return View(list);
        }

    }
}
