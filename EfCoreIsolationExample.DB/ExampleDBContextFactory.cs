using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace EfCoreIsolationExample.DB;

public class ExampleDBContextFactory
    : IDesignTimeDbContextFactory<ExampleDBContext>
{
    public ExampleDBContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<ExampleDBContext>();

        optionsBuilder.UseSqlServer();

        return new ExampleDBContext(optionsBuilder.Options);
    }
}
