using ControlSystemTournament.Core.Interfaces;
using ControlSystemTournament.Core.Models;


namespace ControlSystemTournament.Core.Services
{
    public class SponsorService : ISponsorService
    {
        private readonly IRepository _context;

        public SponsorService(IRepository context)
        {
            _context = context;
        }

      
        public Task<IEnumerable<Sponsor>> GetAllSponsorsTournamentAsync(Tournament tournament)
        {
            if (tournament == null)
            {
                throw new ArgumentNullException(nameof(tournament), "Tournament cannot be null");
            }

            return _context.GetQuery<Sponsor>(s => s.Tournament.Id == tournament.Id);
        }

        public async Task<Sponsor> GetSponsorByIdAsync(int id)
        {
            return await _context.GetById<Sponsor>(id);
        }


        public async Task<Sponsor> CreateSponsorAsync(Sponsor sponsor)
        {
            await _context.Add(sponsor);
            return sponsor;
        }

        public async Task UpdateSponsorAsync(Sponsor sponsor)
        {
            await _context.Update(sponsor);
        }

        public async Task DeleteSponsorAsync(int id)
        {
            var sponsor = await _context.GetById<Sponsor>(id);
            if (sponsor != null)
            {
                _context.Delete<Sponsor>(id);
            }
        }

       
    }
}
