using AutoMapper;
using RugbyManager.Application.Common.Mapping;
using RugbyManager.Domain.Entities;

namespace RugbyManager.Application.Teams.Queries;

public class TeamMemberDto : IMapFrom<TeamMembership>
{
    public string? FirstName { get; set; }
    public string? Surname { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<TeamMembership, TeamMemberDto>()
               .ForMember(d => d.FirstName, o => o.MapFrom(s => s.Player!.FirstName))
               .ForMember(d => d.Surname, o => o.MapFrom(s => s.Player!.Surname));
    }
}