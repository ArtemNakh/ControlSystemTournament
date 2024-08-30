
using ControlSystemTournament.Core.Models;

namespace ControlSystemTournament.Core.Interfaces
{
    public interface IMatchService
    {
        Task<IEnumerable<Match>> GetAllMatchesTournamentAsync(Tournament tournament);
        Task<Match> GetMatchByIdAsync(int id);
        Task<Match> CreateMatchAsync(Match match);
        Task UpdateMatchAsync(Match match);
        Task DeleteMatchAsync(int id);
    }

}
