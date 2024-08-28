using ControlSystemTournament.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControlSystemTournament.Core.Interfaces
{
    public interface ITeamService
    {
        Task<IEnumerable<Team>> GetTeamsTournamentAsync(Tournament tournament);
        Task<Team> GetTeamByIdAsync(int id);
        Task<Team> CreateTeamAsync(Team team);
        Task UpdateTeamAsync(Team team);
        Task DeleteTeamAsync(int id);

    }

}
