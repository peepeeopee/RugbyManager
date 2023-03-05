using MediatR;
using RugbyManager.Domain.Entities;

namespace RugbyManager.Application.Teams.Queries;

public class GetTeamsQuery : IRequest<List<TeamDto>>
{
    public int Id { get; set; }
}