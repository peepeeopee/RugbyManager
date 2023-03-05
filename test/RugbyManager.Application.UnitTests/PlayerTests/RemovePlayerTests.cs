using FluentAssertions;
using RugbyManager.Application.Players.Commands;
using RugbyManager.Domain.Entities;
using RugbyManager.Domain.Exceptions;

namespace RugbyManager.Application.UnitTests.PlayerTests;

public class RemovePlayerTests : BaseTest
{
    [Fact]
    public async Task RemovePlayer_GivenNonExistingPlayer_PlayerDeleted()
    {
        RemovePlayerCommand command = new()
        {
            PlayerId = 69
        };

        RemovePlayerCommandHandler handler = new(testDbContext);

        var act = handler.Awaiting(x => x.Handle(command, CancellationToken.None));

        await act.Should()
                 .ThrowAsync<PlayerNotFoundException>();
    }

    [Fact]
    public async Task RemovePlayer_GivenExistingPlayer_PlayerDeleted()
    {
        //set up db with player to delete
        Player playerToDelete = new()
        {
            FirstName = "Os",
            Surname = "Du Randt",
            Height = 190
        };
        testDbContext.Players.Add(playerToDelete);
        await testDbContext.SaveChangesAsync(CancellationToken.None);

        RemovePlayerCommand command = new()
        {
            PlayerId = playerToDelete.Id
        };

        RemovePlayerCommandHandler handler = new(testDbContext);

        await handler.Handle(command, CancellationToken.None);

    }
}