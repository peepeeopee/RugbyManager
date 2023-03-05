﻿namespace RugbyManager.Domain.Entities;

public class TeamMembership : BaseEntity
{
    public int PlayerId { get; set; }
    public Player? Player { get; set; }
    public int TeamId { get; set; }
    public Team? Team { get; set; }
}