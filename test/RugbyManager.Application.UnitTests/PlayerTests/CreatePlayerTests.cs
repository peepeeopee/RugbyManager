using FluentAssertions;
using RugbyManager.Application.Players.Commands;
using RugbyManager.Application.Teams.Commands;
using RugbyManager.Domain.Entities;
using RugbyManager.Domain.Exceptions;

namespace RugbyManager.Application.UnitTests.PlayerTests;

[Collection("Sequential")]
public class CreatePlayerTests : BaseTest
{
    [Fact]
    public async Task CreatePlayer_GivenPlayerWhichDoesntExist_IdReturned()
    {
        var teamName = "New Team";

        AddPlayerCommand command = new()
        {
            FirstName = "Johah",
            Surname = "Lomu",
            Height = 196
        };

        AddPlayerCommandHandler handler = new (TestDbContext, mapper);

        var teamId = await handler.Handle(command, CancellationToken.None);

        teamId.Should()
              .NotBe(0);
    }
}