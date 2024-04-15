using System.Diagnostics;
using Microsoft.EntityFrameworkCore;
using TestEFBreakingChanges.Models;

namespace TestEFBreakingChanges.DataAccess;

public class StoreContext : DbContext
{
    private readonly string _connectionString;

    public DbSet<Store> Stores => Set<Store>();

    public StoreContext(
        string connectionString
    )
    {
        _connectionString = connectionString;
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.LogTo(message => Debug.WriteLine(message));

        optionsBuilder
            .UseMySql(_connectionString, ServerVersion.AutoDetect(_connectionString));
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(StoreContext).Assembly);
    }
}
