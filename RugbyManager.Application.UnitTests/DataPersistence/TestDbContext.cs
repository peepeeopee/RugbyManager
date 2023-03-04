using Microsoft.EntityFrameworkCore;
using RugbyManager.Application.Interfaces;
using RugbyManager.Domain.Entities;

namespace RugbyManager.Domain.DataPersistence;

public class TestDbContext : DbContext, IAppContext
{
    public DbSet<Player> Players { get; set; }
    public DbSet<Stadium> Stadiums { get; set; }
    public DbSet<Team> Teams { get; set; }
    public DbSet<Transfer> Transfers { get; set; }

    public async Task SaveChangesAsync(CancellationToken token = default) =>
        await base.SaveChangesAsync(token);


    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseInMemoryDatabase("TestDb");
    }
}