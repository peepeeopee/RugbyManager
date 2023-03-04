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

        await AppDbContext.Stadiums.AddAsync(new Stadium()
        {
            Name = stadiumName,
            Capacity = 420
        });

        await AppDbContext.SaveChangesAsync();

        var command = new AddStadiumCommand(stadiumName, 420);

        var handler = new AddStadiumCommandHandler(AppDbContext, mapper);

        var act = handler.Awaiting(x => x.Handle(command, CancellationToken.None));

        await act.Should()
                 .ThrowAsync<StadiumAlreadyExistsException>();
    }

    [Fact]
    public async Task AddStadium_GivenStadiumNameWhichDoesntExist_IdReturned()
    {
        var teamName = "New Stadium";

        var command = new AddStadiumCommand(teamName, 420);

        var handler = new AddStadiumCommandHandler(AppDbContext, mapper);

        var stadiumId = await handler.Handle(command, CancellationToken.None);

        stadiumId.Should()
              .NotBe(0);
    }
}