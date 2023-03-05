using Microsoft.EntityFrameworkCore;
using RugbyManager.Application.Common.Interfaces;
using RugbyManager.Domain.Entities;

namespace RugbyManager.Infrastructure.DataPersistence;

public class AppDbContext : DbContext, IAppDbContext
{
    public DbSet<Player> Players => Set<Player>();
    public DbSet<Stadium> Stadiums => Set<Stadium>();
    public DbSet<Team> Teams => Set<Team>();
    public DbSet<Transfer> Transfers => Set<Transfer>();

    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }
}