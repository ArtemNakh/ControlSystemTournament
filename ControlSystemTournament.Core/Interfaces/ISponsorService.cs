using ControlSystemTournament.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControlSystemTournament.Core.Interfaces
{
    public interface ISponsorService
    {
        Task<IEnumerable<Sponsor>> GetAllSponsorsTournamentAsync(Tournament tournament);
        Task<Sponsor> GetSponsorByIdAsync(int id);
        Task<Sponsor> CreateSponsorAsync(Sponsor sponsor);
        Task UpdateSponsorAsync(Sponsor sponsor);
        Task DeleteSponsorAsync(int id);
    }

}
