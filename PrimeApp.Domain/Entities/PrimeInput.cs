using PrimeApp.Domain.Common;

namespace PrimeApp.Domain.Entities;

public class PrimeInput:IEntity
{
    public Guid Id { get; set; }

    public string InputNumbers { get; set; } = null!;
    public int? MaxPrime { get; set; } 

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;


    public Guid UserId { get; set; }

}