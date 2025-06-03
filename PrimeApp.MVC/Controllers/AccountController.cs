using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using PrimeApp.Infrastructure.Identity;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace PrimeApp.MVC.Controllers;

public class AccountController : Controller
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly SignInManager<ApplicationUser> _signInManager;
    private readonly IConfiguration _config;

    public AccountController(UserManager<ApplicationUser> userManager,
                             SignInManager<ApplicationUser> signInManager,
                             IConfiguration config)
    {
        _userManager = userManager;
        _signInManager = signInManager;
        _config = config;
    }

    [HttpGet]
    public IActionResult Register()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Register(string email, string password, string role = "User")
    {
        var existingUser = await _userManager.FindByEmailAsync(email);
        if (existingUser != null)
        {
            ViewBag.Error = "Bu e-posta zaten kayıtlı.";
            return View();
        }

        var user = new ApplicationUser
        {
            Id = Guid.NewGuid(),
            Email = email,
            UserName = email,
            Role = role
        };

        var result = await _userManager.CreateAsync(user, password);
        if (!result.Succeeded)
        {
            ViewBag.Error = string.Join("<br/>", result.Errors.Select(e => e.Description));
            return View();
        }

        await _userManager.AddToRoleAsync(user, role);

        ViewBag.Success = "Kayıt başarılı. Giriş yapabilirsiniz.";
        return View();
    }

    [HttpGet]
    public IActionResult Login()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Login(string email, string password)
    {
        var user = await _userManager.FindByEmailAsync(email);
        if (user == null)
        {
            ViewBag.Error = "Kullanıcı bulunamadı.";
            return View();
        }

        var result = await _signInManager.CheckPasswordSignInAsync(user, password, false);
        if (!result.Succeeded)
        {
            ViewBag.Error = "Hatalı şifre.";
            return View();
        }

        var token = GenerateJwtToken(user);

        // JWT'yi Cookie olarak ekle
        Response.Cookies.Append("access_token", token, new CookieOptions
        {
            HttpOnly = true,
            Secure = true,
            SameSite = SameSiteMode.Strict,
            Expires = DateTime.UtcNow.AddMinutes(60)
        });

        return RedirectToAction("Index", "Prime");
    }
    
    [Authorize]
    public IActionResult Logout()
    {
        Response.Cookies.Delete("access_token");
        return RedirectToAction("Login", "Account");
    }


    private string GenerateJwtToken(ApplicationUser user)
    {
        var jwtSettings = _config.GetSection("JwtSettings");
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings["Secret"]!));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var claims = new List<Claim>
{
    new Claim(JwtRegisteredClaimNames.Sub, user.Email!), // User.Identity.Name için kullanılabilir
    new Claim("uid", user.Id.ToString()),                // Guid formatındaki user ID
    new Claim(ClaimTypes.Role, user.Role),
    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
};


        var token = new JwtSecurityToken(
            issuer: jwtSettings["Issuer"],
            audience: jwtSettings["Audience"],
            claims: claims,
            expires: DateTime.UtcNow.AddMinutes(60),
            signingCredentials: creds
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}