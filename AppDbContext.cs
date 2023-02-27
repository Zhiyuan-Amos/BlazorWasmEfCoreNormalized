using System.Reflection;
using Microsoft.EntityFrameworkCore;

namespace BlazorWasmEfCoreNormalized;

public sealed class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options) =>
        ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;

    public DbSet<Parent> Parents { get; set; } = default!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }
}