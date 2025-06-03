using MediatR;
using PrimeApp.Application.DTOs;

namespace PrimeApp.Application.Features.PrimeInputs.Commands;

public class CalculateMaxPrimeCommand : IRequest<PrimeInputDto>
{
    public Guid UserId { get; set; }
    public string InputNumbers { get; set; } = null!;

}
