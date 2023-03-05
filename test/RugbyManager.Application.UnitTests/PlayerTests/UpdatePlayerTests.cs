using System.Runtime.InteropServices;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using RugbyManager.Application.Players.Commands;
using RugbyManager.Application.UnitTests.DataPersistence;
using RugbyManager.Domain.Entities;
using RugbyManager.Domain.Exceptions;

namespace RugbyManager.Application.UnitTests.PlayerTests;

public class UpdatePlayerTests : BaseTest
{
    [Fact]
    public async Task UpdatePlayer_GivenNonExistingPlayer_PlayerNotFoundExceptionThrown()
    {
        UpdatePlayerCommand command = new()
        {
            PlayerId = 69,
            FirstName = "Arnold",
            Surname = "Schwarzenegger",
            Height = 188
        };

        UpdatePlayerCommandHandler handler = new(_testDbContext);

        var act = handler.Awaiting(x => x.Handle(command, CancellationToken.None));

        await act.Should()
                 .ThrowAsync<PlayerNotFoundException>();
    }

    [Fact]
    public async Task UpdatePlayer_GivenExistingPlayer_PlayerUpdated()
    {
        Player player = new()
        {
            FirstName = "Arnold",
            Surname = "Schwarzenegger",
            Height = 188
        };

        await _testDbContext.Players.AddAsync(player);

        await _testDbContext.SaveChangesAsync();

        UpdatePlayerCommand command = new()
        {
            PlayerId = player.Id,
            FirstName = "Arnie",
            Surname = "Schwarzenegger",
            Height = 188
        };

        UpdatePlayerCommandHandler handler = new(_testDbContext);

        await handler.Handle(command, CancellationToken.None);

        var updatedPlayer = await _testDbContext.Players.FirstOrDefaultAsync(x => x.Id == player.Id);

        updatedPlayer!.FirstName.Should()
                     .Be(command.FirstName);
    }
}