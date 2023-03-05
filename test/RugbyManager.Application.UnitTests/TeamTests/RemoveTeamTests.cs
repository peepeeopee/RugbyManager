using FluentAssertions;
using RugbyManager.Application.Teams.Commands;
using RugbyManager.Application.UnitTests.DataPersistence;
using RugbyManager.Domain.Entities;
using RugbyManager.Domain.Exceptions;

namespace RugbyManager.Application.UnitTests.TeamTests;

public class RemoveTeamTests : BaseTest
{
    [Fact]
    public async Task RemoveTeam_GivenTeamWhichDoesntExist_TeamNotFoundExceptionThrown()
    {
        RemoveTeamCommand command = new()
        {
            TeamId = 21312
        };

        RemoveTeamCommandHandler handler = new(_testDbContext);

        var act =
            handler.Awaiting(x => x.Handle(command, CancellationToken.None)
            );

        await act.Should()
                 .ThrowAsync<TeamNotFoundException>();
    }

    [Fact]
    public async Task RemoveTeam_GivenTeamExists_TeamRemoved()
    {
        Team team = new()
        {
            Name = "ToBeDeleted"
        };

        await _testDbContext.Teams.AddAsync(team);

        await _testDbContext.SaveChangesAsync();

        RemoveTeamCommand command = new()
        {
            TeamId = team.Id
        };

        RemoveTeamCommandHandler handler = new(_testDbContext);

        await handler.Handle(command, CancellationToken.None);
    }
}