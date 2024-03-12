using EfCoreIsolationExample.DB.Entities;
using Microsoft.EntityFrameworkCore;

namespace EfCoreIsolationExample.DB;

public class ExampleDBContext : DbContext
{
    public DbSet<PriceGroup> PriceGroups => Set<PriceGroup>();

    public DbSet<Price> Prices => Set<Price>();

    public ExampleDBContext(DbContextOptions<ExampleDBContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder
            .Entity<Price>()
            .HasOne(price => price.PriceGroup)
            .WithMany()
            .HasForeignKey(price => price.PriceGroupID);
    }
}
