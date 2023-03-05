using FluentAssertions;
using RugbyManager.Application.Stadiums.Commands;
using RugbyManager.Application.Teams.Commands;
using RugbyManager.Domain.Entities;
using RugbyManager.Domain.Exceptions;

namespace RugbyManager.Application.UnitTests.StadiumTests;

[Collection("Sequential")]
public class CreateStadiumTests : BaseTest
{
    [Fact(Skip = "Moving to integration tests")]
    public async Task CreateStadium_GivenStadiumNameForExistingTeam_ErrorThrown()
    {
        var stadiumName = "Existing Stadium";

        await TestDbContext.Stadiums.AddAsync(new Stadium()
        {
            Name = stadiumName,
            Capacity = 420
        });

        await TestDbContext.SaveChangesAsync();

        AddStadiumCommand command = new ()
        {
            Name = stadiumName,
            Capacity = 420
        };

        var handler = new AddStadiumCommandHandler(TestDbContext, mapper);

        var act = handler.Awaiting(x => x.Handle(command, CancellationToken.None));

        await act.Should()
                 .ThrowAsync<StadiumAlreadyExistsException>();
    }

    [Fact]
    public async Task AddStadium_GivenStadiumNameWhichDoesntExist_IdReturned()
    {
        var stadiumName = "New Stadium";

        AddStadiumCommand command = new()
        {
            Name = stadiumName,
            Capacity = 420
        };

        var handler = new AddStadiumCommandHandler(TestDbContext, mapper);

        var stadiumId = await handler.Handle(command, CancellationToken.None);

        stadiumId.Should()
              .NotBe(0);
    }
}