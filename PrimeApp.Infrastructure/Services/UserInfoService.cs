using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using PrimeApp.Application.Services;
using PrimeApp.Infrastructure.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrimeApp.Infrastructure.Services;

public class UserInfoService : IUserInfoService
{
    private readonly UserManager<ApplicationUser> _userManager;

    public UserInfoService(UserManager<ApplicationUser> userManager)
    {
        _userManager = userManager;
    }

    public async Task<List<(Guid Id, string Email)>> GetUserIdsAndEmailsAsync(CancellationToken cancellationToken)
    {
        return await _userManager.Users
            .Select(x => new ValueTuple<Guid, string>(x.Id, x.Email!))
            .ToListAsync(cancellationToken);
    }
}
