using MediatR;
using PrimeApp.Application.Services;
using PrimeApp.Domain.Common;
using PrimeApp.MVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrimeApp.Application.Features.PrimeInputs.Queries;

public class GetAllPrimeInputsWithUserQuery : IRequest<List<PrimeInputViewModel>>
{
    public class GetAllPrimeInputsWithUserQueryHandler : IRequestHandler<GetAllPrimeInputsWithUserQuery, List<PrimeInputViewModel>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IUserInfoService _userInfoService;

        public GetAllPrimeInputsWithUserQueryHandler(IUnitOfWork unitOfWork, IUserInfoService userInfoService)
        {
            _unitOfWork = unitOfWork;
            _userInfoService = userInfoService;
        }

        public async Task<List<PrimeInputViewModel>> Handle(GetAllPrimeInputsWithUserQuery request, CancellationToken cancellationToken)
        {
            var inputs = await _unitOfWork.Repository<PrimeApp.Domain.Entities.PrimeInput>()
                .GetAllAsync();

            var users = await _userInfoService.GetUserIdsAndEmailsAsync(cancellationToken);

            var result = inputs
                .Join(users,
                    input => input.UserId,
                    user => user.Id,
                    (input, user) => new PrimeInputViewModel
                    {
                        UserId = input.UserId,
                        UserEmail = user.Email,
                        InputNumbers = input.InputNumbers,
                        MaxPrime = input.MaxPrime,
                        CreatedAt = input.CreatedAt
                    })
                .OrderByDescending(x => x.CreatedAt)
                .ToList();

            return result;
        }
    }
}
