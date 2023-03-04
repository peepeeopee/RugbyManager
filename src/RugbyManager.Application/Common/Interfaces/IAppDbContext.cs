using Microsoft.EntityFrameworkCore;
using RugbyManager.Domain.Entities;

namespace RugbyManager.Application.Common.Interfaces;

public interface IAppDbContext
{
    DbSet<Player> Players { get; }
    DbSet<Stadium> Stadiums { get; }
    DbSet<Team> Teams { get; }
    DbSet<Transfer> Transfers { get; }

    Task<int> SaveChangesAsync(CancellationToken token = default);
}