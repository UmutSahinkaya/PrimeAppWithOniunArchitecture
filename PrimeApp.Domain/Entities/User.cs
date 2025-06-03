using PrimeApp.Domain.Common;
using PrimeApp.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrimeApp.Domain.Entities;

public class User:IEntity
{
    public Guid Id { get; set; }
    public string Email { get; set; } = null!;
    public string PasswordHash { get; set; } = null!;
    public Role Role { get; set; } = Role.User;

    public ICollection<PrimeInput> PrimeInputs { get; set; }
    
}
