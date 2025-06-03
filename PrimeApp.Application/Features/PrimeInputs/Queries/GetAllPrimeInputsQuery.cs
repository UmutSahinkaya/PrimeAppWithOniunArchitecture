using MediatR;
using PrimeApp.Domain.Common;
using PrimeApp.Domain.Entities;
using PrimeApp.MVC.Models;

namespace PrimeApp.Application.Features.PrimeInputs.Queries;

public class GetAllPrimeInputsQuery : IRequest<List<PrimeInputViewModel>>
{
    public class GetAllPrimeInputsQueryHandler : IRequestHandler<GetAllPrimeInputsQuery, List<PrimeInputViewModel>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetAllPrimeInputsQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<List<PrimeInputViewModel>> Handle(GetAllPrimeInputsQuery request, CancellationToken cancellationToken)
        {
            var inputs = await _unitOfWork.Repository<PrimeInput>().GetAllAsync();

            return inputs.Select(input => new PrimeInputViewModel
            {
                UserId = input.UserId,
                InputNumbers = input.InputNumbers,
                MaxPrime = input.MaxPrime,
                CreatedAt = input.CreatedAt
            })
            .OrderByDescending(x => x.CreatedAt)
            .ToList();
        }
    }
}
