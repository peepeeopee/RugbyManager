using AutoMapper;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using RugbyManager.Application.Common.Mapping;
using RugbyManager.Application.Teams.Queries;
using RugbyManager.Application.UnitTests.DataPersistence;
using RugbyManager.Domain.Entities;
using RugbyManager.Domain.Exceptions;

namespace RugbyManager.Application.UnitTests.TeamTests;

public class GetTeamTests : BaseTest
{
    [Fact]
    public async Task GetTeams_QuerySentWhenTeamsExist_TeamsReturned()
    {

        await _testDbContext.Teams.AddAsync(new()
        {
            Name = "Stormers",
        });
        await _testDbContext.Teams.AddAsync(new()
        {
            Name = "Crusaders",
        });

        await _testDbContext.SaveChangesAsync();

        GetTeamsQuery query = new();

        GetTeamsQueryHandler handler = new(_testDbContext, _mapper);

        var results = await handler.Handle(query, CancellationToken.None);

        results.Should().NotBeEmpty();
    }

    [Fact]
    public async Task GetTeam_QuerySentWithNonExistingTeam_TeamNotFoundExceptionThrown()
    {

        GetTeamByIdQuery query = new()
        {
            TeamId = 456
        };

        GetTeamByIdQueryHandler handler = new(_testDbContext, _mapper);

        var act = handler.Awaiting(x => x.Handle(query, CancellationToken.None));

        await act.Should()
                 .ThrowAsync<TeamNotFoundException>();
    }

    [Fact]
    public async Task GetTeam_QuerySentWithExistingTeam_TeamNotFoundExceptionThrown()
    {

        Team team = new()
        {
            Name = "Cheetahs",
        };

        await _testDbContext.Teams.AddAsync(team);

        await _testDbContext.SaveChangesAsync();

        GetTeamByIdQuery query = new()
        {
            TeamId = team.Id
        };

        GetTeamByIdQueryHandler handler = new(_testDbContext, _mapper);

        var returnedTeam = await handler.Handle(query, CancellationToken.None);

        returnedTeam.Name.Should()
                    .Be(team.Name);
    }

}
