using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using PrimeApp.Domain.Entities;
using PrimeApp.Infrastructure.Identity;

namespace PrimeApp.Infrastructure.Persistence;

public class ApplicationDbContext : IdentityDbContext<ApplicationUser, IdentityRole<Guid>, Guid>
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

    public DbSet<PrimeInput> PrimeInputs => Set<PrimeInput>();

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.Entity<ApplicationUser>().Property(x => x.Role).IsRequired();

        builder.Entity<PrimeInput>()
    .HasOne<ApplicationUser>() // Navigation property yoksa burası böyle yazılır
    .WithMany()
    .HasForeignKey(p => p.UserId)
    .OnDelete(DeleteBehavior.Restrict);
    }
}
