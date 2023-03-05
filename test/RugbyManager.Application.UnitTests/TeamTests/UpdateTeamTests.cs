using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using RugbyManager.Application.Teams.Commands;
using RugbyManager.Domain.Entities;
using RugbyManager.Domain.Exceptions;

namespace RugbyManager.Application.UnitTests.TeamTests;

public class UpdateTeamTests : BaseTest
{
    [Fact]
    public async Task UpdateTeam_GivenTeamWhichDoesntExist_TeamNotFoundExceptionThrown()
    {
        UpdateTeamCommand command = new()
        {
            Name = "Not Existing Team",
            TeamId = 345,
        };

        UpdateTeamCommandHandler handler = new(_testDbContext);

        var act = handler.Awaiting(x => x.Handle(command, CancellationToken.None)
        );

        await act.Should()
                 .ThrowAsync<TeamNotFoundException>();
    }

    [Fact]
    public async Task UpdateTeam_GivenTeamWhichExists_TeamUpdated()
    {
        Team team = new()
        {
            Name = "Golden Lions"
        };

        await _testDbContext.Teams.AddAsync(team);
        await _testDbContext.SaveChangesAsync();

        UpdateTeamCommand command = new()
        {
            Name = "Xerox Golden Lions",
            TeamId = team.Id,
        };

        UpdateTeamCommandHandler handler = new(_testDbContext);

        await handler.Handle(command, CancellationToken.None);

        var updatedTeam = await _testDbContext.Teams.FirstOrDefaultAsync(t => t.Id == team.Id);

        updatedTeam!.Name.Should()
                    .Be(command.Name);
    }
}