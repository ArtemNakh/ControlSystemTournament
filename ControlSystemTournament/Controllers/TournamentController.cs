using AutoMapper;
using ControlSystemTournament.Core.Interfaces;
using ControlSystemTournament.Core.Models;
using ControlSystemTournament.Core.Services;
using ControlSystemTournament.DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace ControlSystemTournament.Controllers
{
    [Route("api/Tournament")]
    [ApiController]
    public class TournamentController : ControllerBase
    {
        private readonly ITournamentService _tournamentService;
        private readonly ILocationService _locationService;
        private readonly IMapper _mapper;
        private readonly ISponsorService _sponsorService;

        public TournamentController(ITournamentService tournamentService, IMapper mapper, ILocationService locationService, ISponsorService sponsorService)
        {
            _tournamentService = tournamentService;
            _mapper = mapper;
            _locationService = locationService;
            _sponsorService = sponsorService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<TournamentDTO>>> GetTournaments()
        {
            var tournaments = await _tournamentService.GetAllTournamentsAsync();
            var tournamentDTOs = _mapper.Map<IEnumerable<TournamentDTO>>(tournaments);
            return Ok(tournamentDTOs);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<TournamentDTO>> GetTournament(int id)
        {
            var tournament = await _tournamentService.GetTournamentByIdAsync(id);
            if (tournament == null)
            {
                return NotFound();
            }
            var tournamentDTO = _mapper.Map<TournamentDTO>(tournament);
            return Ok(tournamentDTO);
        }

        [HttpPost]
        public async Task<ActionResult<TournamentDTO>> CreateTournament([FromBody] TournamentDTO tournamentDTO)
        {
            if (tournamentDTO == null)
            {
                return BadRequest();
            }

            var tournament = _mapper.Map<Tournament>(tournamentDTO);
            Location locations;

            try
            {
                locations = await _locationService.GetLocationByIdAsync(tournament.Location.Id);
            }
            catch (Exception)
            {
                throw new Exception("Location does not found");
            }
          
            tournament.Location = locations;
            var createdTournament = await _tournamentService.CreateTournamentAsync(tournament);
            tournamentDTO = _mapper.Map<TournamentDTO>(createdTournament);
            return Created("Tournament created", tournamentDTO);
        }



        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteTournament(int id)
        {
            var tournament = await _tournamentService.GetTournamentByIdAsync(id);
            if (tournament == null)
            {
                return NotFound();
            }

            var sponsors = await _sponsorService.GetAllSponsorsTournamentAsync(tournament);
            foreach (var sponsor in sponsors)
                await _sponsorService.DeleteSponsorAsync(sponsor.Id);


            await _tournamentService.DeleteTournamentAsync(id);
            return NoContent();
        }
       
    }
}
