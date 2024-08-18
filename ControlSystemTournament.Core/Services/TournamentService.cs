
    using ControlSystemTournament.Core.Interfaces;
    using ControlSystemTournament.Core.Models;


    
    namespace ControlSystemTournament.Core.Services
    {
        public class TournamentService : ITournamentService
        {
            private readonly IRepository _context;

            public TournamentService(IRepository context)
            {
                _context = context;
            }

            public async Task<IEnumerable<Tournament>> GetAllTournamentsAsync()
            {
            return  (IEnumerable<Tournament>) _context.GetAll<Tournament>();
            }

            public async Task<Tournament> GetTournamentByIdAsync(int id)
            {
                return await _context.GetById<Tournament>(id);
            }

            public async Task<Tournament> CreateTournamentAsync(Tournament tournament)
            {
                await _context.Add(tournament);
                return tournament;
            }

            public async Task UpdateTournamentAsync(Tournament tournament)
            {
               await _context.Update(tournament);
            }

            public async Task DeleteTournamentAsync(int id)
            {
            var tournament = await _context.GetById<Tournament>(id);
                if (tournament != null)
                {
                    await _context.Delete<Tournament>(id);
                   
                }
            }
        }


    }


