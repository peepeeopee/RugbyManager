using FluentAssertions;
using RugbyManager.Application.Players.Commands;
using RugbyManager.Application.Teams.Commands;
using RugbyManager.Domain.Entities;
using RugbyManager.Domain.Exceptions;

namespace RugbyManager.Application.UnitTests.PlayerTests;

public class CreatePlayerTests : BaseTest
{
    [Fact]
    public async Task CreatePlayer_GivenPlayerWhichDoesntExist_IdReturned()
    {
        AddPlayerCommand command = new()
        {
            FirstName = "Johah",
            Surname = "Lomu",
            Height = 196
        };

        AddPlayerCommandHandler handler = new (_testDbContext, _mapper);

        var teamId = await handler.Handle(command, CancellationToken.None);

        teamId.Should()
              .NotBe(0);
    }
}