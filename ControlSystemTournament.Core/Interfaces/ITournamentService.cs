using ControlSystemTournament.Core.Models;

namespace ControlSystemTournament.Core.Interfaces
{
    public interface ITournamentService
    {
        Task<IEnumerable<Tournament>> GetAllTournamentsAsync();
        Task<Tournament> GetTournamentByIdAsync(int id);
        Task<Tournament> CreateTournamentAsync(Tournament tournament);
        Task UpdateTournamentAsync(Tournament tournament);
        Task DeleteTournamentAsync(int id);
    }

}
