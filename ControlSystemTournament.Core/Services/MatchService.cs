using ControlSystemTournament.Core.Interfaces;
using ControlSystemTournament.Core.Models;

namespace ControlSystemTournament.Core.Services
{
    public class MatchService : IMatchService
    {
        private readonly IRepository _context;

        public MatchService(IRepository context)
        {
            _context = context;
        }

     

        public async Task<Match> GetMatchByIdAsync(int id)
        {
            return await _context.GetById<Match>(id);
        }


        //todo(create new object and check value)
        public async Task<Match> CreateMatchAsync(Match match)
        {
            await _context.Add(match);
            return match;
        }

        public async Task UpdateMatchAsync(Match match)
        {
            await _context.Update(match);
        }

        public async Task DeleteMatchAsync(int id)
        {
            var match = await _context.GetById<Match>(id);
            if (match != null)
                await _context.Delete<Match>(id);

        }

        public Task<IEnumerable<Match>> GetAllMatchesTournamentAsync(Tournament tournament)
        {
            var matches = _context.GetQuery<Match>(m => m.Tournament.Id == tournament.Id);
            return matches;
        }

       
    }
}
