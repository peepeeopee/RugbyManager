using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using RugbyManager.Application.Transfers.Commands;
using RugbyManager.Application.UnitTests.DataPersistence;
using RugbyManager.Domain.Entities;
using RugbyManager.Domain.Exceptions;

namespace RugbyManager.Application.UnitTests.TransferTests;

[Collection("Sequential")]
public class AddTransferTests : BaseTest
{
    [Fact]
    public async Task TransferPlayer_TransferToNonExistingTeam_TeamNotFoundExceptionThrown()
    {
        Player player = new()
        {
            FirstName = "Johnny",
            Surname = "Wilkinson",
            Height = 178
        };
        await testDbContext.Players.AddAsync(player);
        await testDbContext.SaveChangesAsync();

        TransferPlayerCommand command = new()
        {
            PlayerId = player.Id,
            TeamId = 420
        };

        TransferPlayerCommandHandler handler = new(testDbContext);

        var act = handler.Awaiting(x => x.Handle(command, CancellationToken.None)
        );

        await act.Should()
                 .ThrowAsync<TeamNotFoundException>();

    }

    [Fact]
    public async Task TransferPlayer_TransferToNonExistingPlayer_PlayerNotFoundExceptionThrown()
    {
        Team team = new()
        {
            Name = "Sharks"
        };
        await testDbContext.Teams.AddAsync(team);
        await testDbContext.SaveChangesAsync();

        TransferPlayerCommand command = new()
        {
            PlayerId = 69,
            TeamId = team.Id
        };

        TransferPlayerCommandHandler handler = new(testDbContext);

        var act = handler.Awaiting(x => x.Handle(command, CancellationToken.None)
        );

        await act.Should()
                 .ThrowAsync<PlayerNotFoundException>();
    }

    [Fact]
    public async Task TransferPlayer_TransferToExistingTeamForExistinPlayer_PlayerNotFoundExceptionThrown()
    {
        Team team = new()
        {
            Name = "Sharks"
        };
        await testDbContext.Teams.AddAsync(team);
        Player player = new()
        {
            FirstName = "Johnny",
            Surname = "Wilkinson",
            Height = 178
        };
        await testDbContext.Players.AddAsync(player);

        await testDbContext.SaveChangesAsync();

        TransferPlayerCommand command = new()
        {
            PlayerId = player.Id,
            TeamId = team.Id
        };

        TransferPlayerCommandHandler handler = new(testDbContext);

        await handler.Handle(command, CancellationToken.None);

        var teamMembership = await testDbContext.TeamMemberships.FirstOrDefaultAsync(tm =>
            tm.PlayerId == command.PlayerId && tm.TeamId == command.TeamId);

        teamMembership.Should()
                      .NotBeNull();
    }
}