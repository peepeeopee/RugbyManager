using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using RugbyManager.Domain.Entities;

namespace RugbyManager.Infrastructure.DataPersistence;

public class AppDbContextInitializer
{
    private readonly AppDbContext _context;
    private readonly ILogger<AppDbContextInitializer> _logger;

    public AppDbContextInitializer(AppDbContext context, ILogger<AppDbContextInitializer> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task InitialiseAsync()
    {
        try
        {
            if (_context.Database.IsSqlServer())
            {
                await _context.Database.MigrateAsync();
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while initialising the database.");
            throw;
        }
    }

    public async Task SeedAsync()
    {
        try
        {
            await TrySeedAsync();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while seeding the database.");
            throw;
        }
    }

    public async Task TrySeedAsync()
    {
        // Default data
        // Seed, if necessary
        if (!_context.Stadiums.Any() 
            && !_context.Teams.Any()
            && !_context.Players.Any())
        {
            Stadium stadium = new()
            {
                Name = "Eden Park",
                Capacity = 50000
            };

            _context.Stadiums.Add(stadium);

            Stadium ellisPark = new()
            {
                Capacity = 42000,
                Name = "Coca-Cola Park"
            };
            _context.Stadiums.Add(ellisPark);

            await _context.SaveChangesAsync();

            Team team1 = new()
            {
                Name = "Auckland Blues",
                StadiumId = stadium.Id
            };

            _context.Teams.Add(team1);

            Team team2 = new()
            {
                Name = "Golden Lions",
                StadiumId = stadium.Id
            };

            _context.Teams.Add(team2);

            await _context.SaveChangesAsync();

            Player player1 = new()
            {
                FirstName = "Patrick",
                Surname = "Tuipulotu",
                Height = 198
            };
            Player player2 = new()
            {
                FirstName = "Jacques",
                Surname = "Fourie",
                Height = 190
            };
            Player player3 = new()
            {
                FirstName = "Peter",
                Surname = "Antoncich",
                Height = 185
            };
            _context.Players.Add(player1);
            _context.Players.Add(player2);
            _context.Players.Add(player3);

            await _context.SaveChangesAsync();

            TeamMembership teamMembership1 = new()
            {
                PlayerId = player1.Id,
                TeamId = team1.Id
            };
            TeamMembership teamMembership2 = new()
            {
                PlayerId = player2.Id,
                TeamId = team2.Id
            };
            TeamMembership teamMembership3 = new()
            {
                PlayerId = player3.Id,
                TeamId = team2.Id
            };

            await _context.TeamMemberships.AddRangeAsync(new List<TeamMembership>()
            {
                teamMembership1,
                teamMembership2,
                teamMembership3
            });

            await _context.SaveChangesAsync();
        }
    }
}