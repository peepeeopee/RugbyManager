using Microsoft.EntityFrameworkCore;
using RugbyManager.Application.Common.Interfaces;
using RugbyManager.Domain.Entities;

namespace RugbyManager.Application.UnitTests.DataPersistence;

public class TestDbContext : DbContext, IAppDbContext
{
    public DbSet<Player> Players { get; set; }
    public DbSet<Stadium> Stadiums { get; set; }
    public DbSet<Team> Teams { get; set; }
    public DbSet<Transfer> Transfers { get; set; }
    public DbSet<TeamMembership> TeamMemberships { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseInMemoryDatabase("TestDb");
    }
}