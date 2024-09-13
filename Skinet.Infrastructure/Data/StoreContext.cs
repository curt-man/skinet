using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Skinet.Core.Entities;
using Skinet.Infrastructure.Config;

namespace Skinet.Infrastructure.Data;

public class StoreContext : IdentityDbContext<AppUser>
{
    public StoreContext(DbContextOptions options) : base(options)
    {
        
    }
    public DbSet<Product> Products { get; set; }
    public DbSet<Address> Addresses { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ProductConfiguration).Assembly);
    }
}
