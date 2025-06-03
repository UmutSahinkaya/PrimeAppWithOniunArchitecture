using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrimeApp.Application.DTOs;

public class PrimeInputDto
{
    public string InputNumbers { get; set; } = null!;
    public int? MaxPrime { get; set; }
    public DateTime CreatedAt { get; set; }
}
