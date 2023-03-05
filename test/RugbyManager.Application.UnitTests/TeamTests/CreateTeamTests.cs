using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Moq;
using RugbyManager.Application.Common.Interfaces;
using RugbyManager.Application.Teams.Commands;
using RugbyManager.Domain.Entities;
using RugbyManager.Domain.Exceptions;

namespace RugbyManager.Application.UnitTests.TeamTests;

public class CreateTeamTests : BaseTest
{
    [Fact(Skip = "Moving to integration tests")]
    public async Task CreateTeam_GivenTeamNameForExistingTeam_ErrorThrown()
    {
        var teamName = "Existing Team";

        await _testDbContext.Teams.AddAsync(new Team()
        {
            Name = teamName
        });

        await _testDbContext.SaveChangesAsync();

        AddTeamCommand command = new ()
        {
            Name = teamName
        };

        var handler = new AddTeamCommandHandler(_testDbContext,_mapper);

        var act = handler.Awaiting(x => x.Handle(command, CancellationToken.None));

        await act.Should()
                 .ThrowAsync<TeamAlreadyExistsException>();
    }

    [Fact]
    public async Task CreateTeam_GivenTeamNameWhichDoesntExist_IdReturned()
    {
        var teamName = "New Team";

        AddTeamCommand command = new()
        {
            Name = teamName
        };

        var handler = new AddTeamCommandHandler(_testDbContext, _mapper);

        var teamId = await handler.Handle(command, CancellationToken.None);

        teamId.Should()
              .NotBe(0);
    }
}