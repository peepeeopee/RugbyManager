using AutoMapper;
using RugbyManager.Application.Common.Mapping;
using RugbyManager.Domain.Entities;

namespace RugbyManager.Application.Transfers.Queries;

public class TransferDto : IMapFrom<Transfer>
{
    public int Id { get; set; }
    public int? FromTeamId { get; set; }
    public string? FromTeamName { get; set; }
    public int ToTeamId { get; set; }
    public string ToTeamName { get; set; }
    public int PlayerId { get; set; }
    public string PlayerFirstName { get; set; }
    public string PlayerSurname { get; set; }

    public void Mapping(Profile profile) => profile.CreateMap<Transfer, TransferDto>()
                                            .ForMember(d => d.FromTeamName, o => o.MapFrom(s => s.FromTeam != null ? s.FromTeam.Name : ""))
                                            .ForMember(d => d.ToTeamName, o => o.MapFrom(s => s.ToTeam.Name))
                                            .ForMember(d => d.PlayerFirstName, o => o.MapFrom(s => s.Player.FirstName))
                                            .ForMember(d => d.PlayerSurname, o => o.MapFrom(s => s.Player.Surname));
}