using AutoMapper;
using ControlSystemTournament.Core.Interfaces;
using ControlSystemTournament.Core.Models;
using ControlSystemTournament.DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ControlSystemTournament.Controllers
{
    [Route("api/Sponsor")]
    [ApiController]

    public class SponsorController : ControllerBase
    {
        private readonly ISponsorService _sponsorService;
        private readonly ITournamentService _tournamentService;
        private readonly IMapper _mapper;
        public SponsorController(ISponsorService sponsorService, IMapper mapper, ITournamentService tournamentService)
        {
            _sponsorService = sponsorService;
            _mapper = mapper;
            _tournamentService = tournamentService;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<SponsorDTO>> GetSponsorById(int id)
        {
            var sponsor = await _sponsorService.GetSponsorByIdAsync(id);
            if (sponsor == null)
            {
                return NotFound();
            }


            var sponsorDTO = _mapper.Map<SponsorDTO>(sponsor);
            return Ok(sponsorDTO);
        }

        [HttpPost]
        public async Task<ActionResult<SponsorDTO>> CreateSponsor([FromBody] SponsorDTO sponsorDTO)
        {
            if (sponsorDTO == null)
            {
                return BadRequest();
            }
            var sponsor = _mapper.Map<Sponsor>(sponsorDTO);
            sponsor.Tournament = _tournamentService.GetTournamentByIdAsync(sponsor.Tournament.Id).Result;
            await _sponsorService.CreateSponsorAsync(sponsor);

            Tournament tournament = _tournamentService.GetTournamentByIdAsync(sponsor.Tournament.Id).Result;
            tournament.PrizePool += sponsorDTO.Contribution;
            await _tournamentService.UpdateTournamentAsync(tournament);
            return Created("Create Sponsor", sponsorDTO);
        }



        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteSponsor(int id)
        {
            var sponsor = await _sponsorService.GetSponsorByIdAsync(id);
            if (sponsor == null)
            {
                return NotFound();
            }

            Tournament tournament = _tournamentService.GetTournamentByIdAsync(sponsor.Tournament.Id).Result;
            tournament.PrizePool -= sponsor.Contribution;

            await _tournamentService.UpdateTournamentAsync(tournament);
            await _sponsorService.DeleteSponsorAsync(id);
            return NoContent();
        }

        [HttpGet("tournament/{tournamentId}")]
        public async Task<ActionResult<IEnumerable<SponsorDTO>>> GetSponsorsByTournament(int tournamentId)
        {
            var tournament = _tournamentService.GetTournamentByIdAsync(tournamentId).Result;
            var sponsors = await _sponsorService.GetAllSponsorsTournamentAsync(tournament);
            var sponsorsDTO = _mapper.Map<IEnumerable<SponsorDTO>>(sponsors);
            return Ok(sponsorsDTO);
        }

    }
}
