﻿using ControlSystemTournament.Core.Interfaces;
using ControlSystemTournament.Core.Models;
using System.Collections.Generic;


namespace ControlSystemTournament.Core.Services
{
    public class SponsorService : ISponsorService
    {
        private readonly IRepository _context;
        private readonly ITournamentService _tournamentService;

        public SponsorService(IRepository context, ITournamentService tournamentService)
        {
            _context = context;
            _tournamentService =  tournamentService;
        }


        public Task<IEnumerable<Sponsor>> GetAllSponsorsTournamentAsync(Tournament tournament)
        {
            
            if (tournament == null)
            {
                throw new ArgumentNullException(nameof(tournament), "Tournament not found");
            }

            try
            {
                return _context.GetQuery<Sponsor>(s => s.Tournament.Id == tournament.Id);
            }
            catch (Exception)
            {

                throw new Exception("Error find Sponsor");
            }
          
        }

        public async Task<Sponsor> GetSponsorByIdAsync(int id)
        {
            var sponsor = await _context.GetById<Sponsor>(id);
            var tournament = sponsor?.Tournament;  
            return sponsor;
        }


        public async Task<Sponsor> CreateSponsorAsync(Sponsor sponsor)
        {
            if ((_context.GetQuery<Sponsor>(n => n.Name == sponsor.Name && n.Tournament.Id == sponsor.Tournament.Id)).Result.Count() >0)
                throw new Exception("Choose new Name");
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
