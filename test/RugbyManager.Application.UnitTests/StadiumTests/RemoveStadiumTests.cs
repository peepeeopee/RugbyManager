using FluentAssertions;
using RugbyManager.Application.Stadiums.Commands;
using RugbyManager.Domain.Entities;
using RugbyManager.Domain.Exceptions;

namespace RugbyManager.Application.UnitTests.StadiumTests;

[Collection("Sequential")]
public class RemoveStadiumTests : BaseTest
{
    [Fact]
    public async Task RemoveStadium_GivenStadiumWhichDoesntExist_StadiumNotFoundExceptionThrown()
    {
        RemoveStadiumCommand command = new()
        {
            StadiumId = 21312
        };

        RemoveStadiumCommandHandler handler = new(testDbContext);

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

        await testDbContext.Stadiums.AddAsync(stadium);

        await testDbContext.SaveChangesAsync();

        RemoveStadiumCommand command = new()
        {
            StadiumId = stadium.Id
        };

        RemoveStadiumCommandHandler handler = new(testDbContext);

        await handler.Handle(command,CancellationToken.None);
    }
}