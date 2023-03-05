using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using RugbyManager.Application.Stadiums.Commands;
using RugbyManager.Domain.Entities;
using RugbyManager.Domain.Exceptions;

namespace RugbyManager.Application.UnitTests.StadiumTests;

[Collection("Sequential")]
public class UpdateStadiumTests : BaseTest
{
    [Fact]
    public async Task UpdateStadium_GivenStadiumWhichDoesntExist_StadiumNotFoundExceptionThrown()
    {
        UpdateStadiumCommand command = new()
        {
            StadiumId = 123,
            Capacity = 77,
            Name = "Nowhere"
        };

        UpdateStadiumCommandHandler handler = new(testDbContext);

        var act = handler.Awaiting(x =>
            x.Handle(command, CancellationToken.None)
        );

        await act.Should()
                 .ThrowAsync<StadiumNotFoundException>();
    }

    [Fact]
    public async Task UpdateStadium_GivenStadiumWhichExists_StadiumUpdated()
    {
        Stadium stadium = new()
        {
            Capacity = 42000,
            Name = "Ellis Park"
        };

        await testDbContext.Stadiums.AddAsync(stadium);

        await testDbContext.SaveChangesAsync();

        UpdateStadiumCommand command = new()
        {
            StadiumId = stadium.Id,
            Capacity = 50000,
            Name = "Coca-Cola Park"
        };

        UpdateStadiumCommandHandler handler = new(testDbContext);

        await handler.Handle(command, CancellationToken.None);

        var updatedStadium =
            await testDbContext.Stadiums.FirstOrDefaultAsync(s => s.Id == stadium.Id);

        updatedStadium!.Name.Should()
                       .Be(command.Name);
        updatedStadium!.Capacity.Should()
                       .Be(command.Capacity);
    }
}