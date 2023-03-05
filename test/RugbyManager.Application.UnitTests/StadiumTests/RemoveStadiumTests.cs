using FluentAssertions;
using RugbyManager.Application.Stadiums.Commands;
using RugbyManager.Domain.Entities;
using RugbyManager.Domain.Exceptions;

namespace RugbyManager.Application.UnitTests.StadiumTests;

public class RemoveStadiumTests : BaseTest
{
    [Fact]
    public async Task RemoveStadium_GivenStadiumWhichDoesntExist_StadiumNotFoundExceptionThrown()
    {
        RemoveStadiumCommand command = new()
        {
            StadiumId = 21312
        };

        RemoveStadiumCommandHandler handler = new(_testDbContext);

        var act =
            handler.Awaiting(x => x.Handle(command, CancellationToken.None)
            );

        await act.Should()
                 .ThrowAsync<StadiumNotFoundException>();
    }

    [Fact]
    public async Task RemoveStadium_GivenStadiumExists_StadiumRemoved()
    {
        Stadium stadium = new()
        {
            Capacity = 1213,
            Name = "ToBeDeleted"
        };

        await _testDbContext.Stadiums.AddAsync(stadium);

        await _testDbContext.SaveChangesAsync();

        RemoveStadiumCommand command = new()
        {
            StadiumId = stadium.Id
        };

        RemoveStadiumCommandHandler handler = new(_testDbContext);

        await handler.Handle(command,CancellationToken.None);
    }
}