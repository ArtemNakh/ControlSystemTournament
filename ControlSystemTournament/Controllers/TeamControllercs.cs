using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ControlSystemTournament.Core.Interfaces;
using ControlSystemTournament.Core.Models;
using AutoMapper;
using ControlSystemTournament.DTOs;
using ControlSystemTournament.Core.Services;

namespace ControlSystemTournament.Controllers
{
    [Route("api/Team")]
    [ApiController]
    public class TeamController : ControllerBase
    {
        private readonly ITeamService _teamService;
        private readonly IMapper _mapper;
        private readonly ITournamentService _tournamentService;
        private readonly IPlayerService _playerService;

        public TeamController(ITeamService teamService, IMapper mapper, ITournamentService tournamentService, IPlayerService playerService)
        {
            _teamService = teamService;
            _mapper = mapper;
            _tournamentService = tournamentService;
            _playerService = playerService;
        }


        [HttpGet("{id}")]
        public async Task<ActionResult<TeamDTO>> GetTeam(int id)
        {
            var team = await _teamService.GetTeamByIdAsync(id);
            if (team == null)
            {
                return NotFound();
            }
            var teamDTO = _mapper.Map<TeamDTO>(team);
            teamDTO.TournamentId = id;
            return Ok(teamDTO);
        }

        [HttpPost]
        public async Task<ActionResult<TeamDTO>> CreateTeam(TeamDTO teamDTO)
        {
            var team = _mapper.Map<Team>(teamDTO);
            team.Tournament = _tournamentService.GetTournamentByIdAsync(teamDTO.TournamentId).Result;
            if (team.Tournament ==null)
                return NotFound();


            if (_teamService.GetTeamsTournamentAsync(team.Tournament).Result.Where(n => n.NameTeam == team.NameTeam).Count()>0 )
                return BadRequest("Choose other name team");

            await _teamService.CreateTeamAsync(team);

            _mapper.Map<TeamDTO>(team);
            return Created("Created team",teamDTO);
        }


        [HttpPut("{idTeam}")]
        public async Task<ActionResult> AddCouchToTeam(int idTeam, int CouchId)
        {
            if (CouchId ==null || idTeam==null) 
                return BadRequest();

            Team team = _teamService.GetTeamByIdAsync(idTeam).Result;
            if (team == null)
                return NotFound("team not found");
            var coachPlayer= _playerService.GetPlayerByIdAsync(CouchId).Result;
            if (coachPlayer == null)
                return NotFound("Player not found");
            team.Coach= coachPlayer;
            _teamService.UpdateTeamAsync(team);

            return Ok("Couch was successful added");
        }


        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteTeam(int id)
        {
            var team = await _teamService.GetTeamByIdAsync(id);
            if (team == null)
            {
                return NotFound();
            }

            await _teamService.DeleteTeamAsync(id);
            return NoContent();
        }

        [HttpGet("tournament/{tournamentId}")]
        public async Task<ActionResult<TeamDTO>> GetTeamsByTournament(int tournamentId)
        {
           var tournament = await _tournamentService.GetTournamentByIdAsync(tournamentId);
            var teams = await _teamService.GetTeamsTournamentAsync(tournament);
            var teamsDTO= _mapper.Map<IEnumerable<TeamDTO>>(teams);
            return Ok(teamsDTO);
        }
    }
}
