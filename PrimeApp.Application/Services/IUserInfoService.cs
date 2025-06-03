using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrimeApp.Application.Services
{
    public interface IUserInfoService
    {
        Task<List<(Guid Id, string Email)>> GetUserIdsAndEmailsAsync(CancellationToken cancellationToken);
    }
}
