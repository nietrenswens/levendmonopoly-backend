﻿using LevendMonopoly.Api.Models;

namespace LevendMonopoly.Api.Interfaces
{
    public interface ITeamSessionService
    {
        Task<TeamSession?> CreateSessionAsync(Team team);
        Task<TeamSession?> GetSessionAsync(string token);
        Task<bool> DisableSessionAsync(string token);
    }
}
