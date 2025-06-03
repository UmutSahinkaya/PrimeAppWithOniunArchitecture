using Microsoft.AspNetCore.Identity;

namespace PrimeApp.Infrastructure.Identity
{
    public class ApplicationUser : IdentityUser<Guid>
    {
        public string Role { get; set; } = "User";
    }
}
