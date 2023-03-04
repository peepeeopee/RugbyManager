using Microsoft.EntityFrameworkCore;
using RugbyManager.Domain.Entities;

namespace RugbyManager.Application.Interfaces;

public interface IAppContext
{
    DbSet<Player> Players { get; }
    DbSet<Stadium> Stadiums { get; }
    DbSet<Team> Teams { get; }
    DbSet<Transfer> Transfers { get; }

    Task SaveChangesAsync();
}