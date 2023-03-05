using AutoMapper;
using FluentAssertions;
using RugbyManager.Application.Common.Mapping;
using System.Runtime.Serialization;
using RugbyManager.Application.Players.Commands;
using RugbyManager.Application.Stadiums.Commands;
using RugbyManager.Application.Teams.Commands;
using RugbyManager.Application.Common.Models.Player;
using RugbyManager.Application.Common.Models.Stadium;
using RugbyManager.Application.Common.Models.Team;
using RugbyManager.Application.Common.Models.Transfers;
using RugbyManager.Application.Transfers.Commands;

namespace RugbyManager.Application.UnitTests.Mapping;

public class MappingTests
{
    private readonly IMapper _mapper;

    public MappingTests()
    {
        _mapper = new MapperConfiguration(cfg => cfg.AddProfile<MappingProfile>())
            .CreateMapper();
    }

    [Fact]
    public void AssertConfigurationIsValid()
    {
        var act = () => _mapper.ConfigurationProvider.AssertConfigurationIsValid();

        act.Should()
           .NotThrow<AutoMapperConfigurationException>();
    }

    [Theory]
    [InlineData(typeof(AddPlayerRequest), typeof(AddPlayerCommand))]
    [InlineData(typeof(AddStadiumRequest), typeof(AddStadiumCommand))]
    [InlineData(typeof(AddTeamRequest), typeof(AddTeamCommand))]
    [InlineData(typeof(UpdatePlayerRequest), typeof(UpdatePlayerCommand))]
    [InlineData(typeof(UpdateStadiumRequest), typeof(UpdateStadiumCommand))]
    [InlineData(typeof(UpdateTeamRequest), typeof(UpdateTeamCommand))]
    [InlineData(typeof(RemovePlayerRequest), typeof(RemovePlayerCommand))]
    [InlineData(typeof(RemoveStadiumRequest), typeof(RemoveStadiumCommand))]
    [InlineData(typeof(TransferPlayerRequest), typeof(TransferPlayerCommand))]
    public void GivenSourceType_Mapped_ToDestinationType(Type sourceType, Type destinationType)
    {
        var source = GetInstanceOf(sourceType);

        _mapper.Map(source, sourceType, destinationType);
    }

    private object GetInstanceOf(Type type) =>
        type.GetConstructor(Type.EmptyTypes) is not null
            ? Activator.CreateInstance(type)!
            : FormatterServices.GetUninitializedObject(type);
    
}