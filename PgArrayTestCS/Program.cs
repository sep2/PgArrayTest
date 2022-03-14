using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

await using var ctx = new ApplicationDbContext();
await ctx.Database.EnsureCreatedAsync();

var guids = new Guid[]
{
    new("ab02c141-7706-4570-8893-ae60fa741f81"),
    new("0e89bc4e-63dd-4cb4-be17-7108d3e7a90b")
};

// C# is ok
_ = ctx.Events.Where(x => guids.Contains(x.Id)).ToList();

public class ApplicationDbContext : DbContext
{
    public DbSet<Event> Events { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder
            .UseNpgsql(@"Host=localhost;Username=test;Password=test")
            .LogTo(Console.WriteLine, LogLevel.Information)
            .EnableSensitiveDataLogging();
}

public class Event
{
    public Guid Id { get; set; }
}