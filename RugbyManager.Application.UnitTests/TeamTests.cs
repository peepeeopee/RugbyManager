using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Moq;
using RugbyManager.Application.Interfaces;
using RugbyManager.Application.Teams.Commands;
using RugbyManager.Domain.DataPersistence;
using RugbyManager.Domain.Entities;
using RugbyManager.Domain.Exceptions;

namespace RugbyManager.Application.UnitTests;

public class TeamTests : BaseTest
{
    [Fact]
    public async Task CreateTeam_GivenTeamNameForExistingTeam_ErrorThrown()
    {
        var teamName = "Existing Team";

        await appContext.Teams.AddAsync(new Team()
        {
            Name = teamName
        });

        await appContext.SaveChangesAsync();


        var command = new CreateTeamCommand(teamName);

        var handler = new CreateTeamCommandHandler(appContext);

        var act = handler.Awaiting(x => x.Handle(command, CancellationToken.None));

        await act.Should().ThrowAsync<TeamAlreadyExistsException>();
    }
}

public class BaseTest
{
    internal TestDbContext appContext { get; set; } = new TestDbContext();
}