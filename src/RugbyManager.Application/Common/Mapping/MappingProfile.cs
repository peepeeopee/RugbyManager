using System.Xml.Linq;
using AutoMapper;
using RugbyManager.Application.Players.Commands;
using RugbyManager.Application.Stadiums.Commands;
using RugbyManager.Application.Teams.Commands;
using RugbyManager.Domain.Entities;

namespace RugbyManager.Application.Common.Mapping;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<AddStadiumCommand, Stadium>();
        CreateMap<AddPlayerCommand, Player>();
        CreateMap<AddTeamCommand, Team>();
    }
}