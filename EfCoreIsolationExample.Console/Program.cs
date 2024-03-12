using System;
using System.Linq;
using System.Threading.Tasks;
using EfCoreIsolationExample.DB;
using EfCoreIsolationExample.DB.Entities;
using Microsoft.EntityFrameworkCore;

if (args[0] == "new")
{
    await using var dbContext = CreateExampleDBContext();

    await dbContext.Database.MigrateAsync();

    await using var transaction = await dbContext
        .Database
        .BeginTransactionAsync();

    await dbContext.Prices.ExecuteDeleteAsync();
    await dbContext.PriceGroups.ExecuteDeleteAsync();

    await dbContext.Database.ExecuteSqlRawAsync(
        "DBCC CHECKIDENT(Prices, RESEED, 0);" +
        "DBCC CHECKIDENT(PriceGroups, RESEED, 0);" +
        "SET IDENTITY_INSERT PriceGroups ON;");

    for (int i = 1; i <= 2; i++)
    {
        var priceGroup = new PriceGroup
        {
            ID = i,
            PriceVersion = Guid.NewGuid(),
        };

        dbContext.PriceGroups.Add(priceGroup);
    }

    await dbContext.SaveChangesAsync();

    await transaction.CommitAsync();

    return;
}

var date = DateOnly.FromDateTime(DateTimeOffset.Now.DateTime);

var priceGroupID = int.Parse(args[0]);
var percent = decimal.Parse(args[1]);

{
    await using var dbContext = CreateExampleDBContext();

    await using var transaction = await dbContext
        .Database
        .BeginTransactionAsync();
    // Använd den här raden istället för raden ovan för att använda
    // snapshots. Se även
    // EfCoreIsolationExample.DB/Migrations/20240312080213_Price.cs
    //.BeginTransactionAsync(IsolationLevel.Snapshot);

    Console.WriteLine("Före ExecuteUpdateAsync " + percent);

    await dbContext
        .PriceGroups
        .Where(priceGroup => priceGroup.ID == priceGroupID)
        .ExecuteUpdateAsync(properties => properties.SetProperty(
            priceGroup => priceGroup.PriceVersion,
            _ => Guid.NewGuid()));

    Console.WriteLine("Efter ExecuteUpdateAsync " + percent);

    var price = new Price
    {
        Value = percent,
        Start = date,
        End = date,
        PriceGroupID = priceGroupID,
    };

    if (await dbContext
        .Prices
        .AnyAsync(otherPrice => otherPrice.PriceGroupID == price.PriceGroupID
            && price.Start <= otherPrice.End
            && price.Start <= otherPrice.End))
    {
        throw new Exception("Ett överlapp finns.");
    }

    dbContext.Prices.Add(price);

    await dbContext.SaveChangesAsync();

    await Task.Delay(10000 - (int)percent * 1000);

    await transaction.CommitAsync();
}

{
    await using var dbContext = CreateExampleDBContext();
    var count = await dbContext
        .Prices
        .CountAsync(price => price.PriceGroupID == priceGroupID);

    if (count != 1)
    {
        throw new Exception("Fel antal.");
    }
}

static ExampleDBContext CreateExampleDBContext()
{
    var builder = new DbContextOptionsBuilder<ExampleDBContext>();
    // Mata in en ConnectionString till en tom databas här.
    builder.UseSqlServer();

    return new ExampleDBContext(builder.Options);
}
