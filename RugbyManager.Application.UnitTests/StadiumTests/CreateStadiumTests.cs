using FluentAssertions;
using RugbyManager.Application.Stadiums.Commands;
using RugbyManager.Application.Teams.Commands;
using RugbyManager.Domain.Entities;
using RugbyManager.Domain.Exceptions;

namespace RugbyManager.Application.UnitTests.StadiumTests;

[Collection("Sequential")]
public class CreateStadiumTests : BaseTest
{
    [Fact]
    public async Task CreateStadium_GivenStadiumNameForExistingTeam_ErrorThrown()
    {
        var stadiumName = "Existing Stadium";

        await appContext.Stadiums.AddAsync(new Stadium()
        {
            Name = stadiumName,
            Capacity = 420
        });

        await appContext.SaveChangesAsync();

        var command = new AddStadiumCommand(stadiumName, 420);

        var handler = new AddStadiumCommandHandler(appContext);

        var act = handler.Awaiting(x => x.Handle(command, CancellationToken.None));

        await act.Should()
                 .ThrowAsync<StadiumAlreadyExistsException>();
    }

    [Fact]
    public async Task AddStadium_GivenStadiumNameWhichDoesntExist_IdReturned()
    {
        var teamName = "New Stadium";

        var command = new AddStadiumCommand(teamName, 420);

        var handler = new AddStadiumCommandHandler(appContext);

        var stadiumId = await handler.Handle(command, CancellationToken.None);

        stadiumId.Should()
              .NotBe(0);
    }
}