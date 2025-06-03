using MediatR;
using PrimeApp.MVC.Models;

namespace PrimeApp.Application.Features.PrimeInputs.Queries;

public class GetAllPrimeInputsQuery : IRequest<List<PrimeInputViewModel>>
{

}
