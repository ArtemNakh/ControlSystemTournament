using System;
using ControlSystemTournament.Core.Models;

namespace ControlSystemTournament.Core.Interfaces
{
    public interface IMatchService
    {
        //Task<IEnumerable<Match>> GetAllMatchesAsync();
        Task<IEnumerable<Match>> GetAllMatchesTournamentAsync(Tournament tournament);
        //Task<IEnumerable<Team>> GetTeamsMatcheAsync(Match match);
        Task<Match> GetMatchByIdAsync(int id);
        Task<Match> CreateMatchAsync(Match match);
        Task UpdateMatchAsync(Match match);
        Task DeleteMatchAsync(int id);
    }

}
