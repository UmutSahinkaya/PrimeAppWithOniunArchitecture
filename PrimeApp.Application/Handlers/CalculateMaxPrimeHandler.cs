using MediatR;
using PrimeApp.Application.DTOs;
using PrimeApp.Application.Features.PrimeInputs.Commands;
using PrimeApp.Domain.Common;
using PrimeApp.Domain.Entities;

namespace PrimeApp.Application.Handlers
{
    public class CalculateMaxPrimeHandler : IRequestHandler<CalculateMaxPrimeCommand, PrimeInputDto>
    {
        private readonly IUnitOfWork _unitOfWork;

        public CalculateMaxPrimeHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<PrimeInputDto> Handle(CalculateMaxPrimeCommand request, CancellationToken cancellationToken)
        {
            var numbers = request.InputNumbers
                                 .Split(',')
                                 .Select(s => int.TryParse(s.Trim(), out var n) ? n : 0)
                                 .Where(n => n > 1)
                                 .ToList();

            int? maxPrime = numbers.Where(IsPrime).OrderByDescending(n => n).FirstOrDefault();

            var entity = new PrimeInput
            {
                UserId = request.UserId,
                InputNumbers = request.InputNumbers,
                MaxPrime = maxPrime
            };

            await _unitOfWork.Repository<PrimeInput>().AddAsync(entity);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return new PrimeInputDto
            {
                InputNumbers = request.InputNumbers,
                MaxPrime = maxPrime,
                CreatedAt = entity.CreatedAt
            };
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
