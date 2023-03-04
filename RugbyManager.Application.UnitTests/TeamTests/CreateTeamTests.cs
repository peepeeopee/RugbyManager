using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Moq;
using RugbyManager.Application.Common.Interfaces;
using RugbyManager.Application.Teams.Commands;
using RugbyManager.Domain.Entities;
using RugbyManager.Domain.Exceptions;

namespace RugbyManager.Application.UnitTests.TeamTests;

[Collection("Sequential")]
public class CreateTeamTests : BaseTest
{
    [Fact]
    public async Task CreateTeam_GivenTeamNameForExistingTeam_ErrorThrown()
    {
        var teamName = "Existing Team";

        await AppDbContext.Teams.AddAsync(new Team()
        {
            Name = teamName
        });

        await AppDbContext.SaveChangesAsync();

        var command = new AddTeamCommand(teamName);

        var handler = new AddTeamCommandHandler(AppDbContext,mapper);

        var act = handler.Awaiting(x => x.Handle(command, CancellationToken.None));

        await act.Should()
                 .ThrowAsync<TeamAlreadyExistsException>();
    }

    [Fact]
    public async Task CreateTeam_GivenTeamNameWhichDoesntExist_IdReturned()
    {
        var teamName = "New Team";

        var command = new AddTeamCommand(teamName);

        var handler = new AddTeamCommandHandler(AppDbContext, mapper);

        var teamId = await handler.Handle(command, CancellationToken.None);

        teamId.Should()
              .NotBe(0);
    }
}