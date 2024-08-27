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
        public TeamController(ITeamService teamService, IMapper mapper, ITournamentService tournamentService)
        {
            _teamService = teamService;
            _mapper = mapper;
            _tournamentService = tournamentService;
        }

        //[HttpGet]
        //public async Task<ActionResult<IEnumerable<Team>>> GetTeams()
        //{
        //    var teams = await _teamService.GetAllTeamsAsync();
        //    return Ok(teams);
        //}

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
            await _teamService.CreateTeamAsync(team);

            _mapper.Map<TeamDTO>(team);
            return Created("Created team",teamDTO);
        }

        //[HttpPut("{id}")]
        //public async Task<ActionResult> UpdateTeam(int id, [FromBody] TeamDTO teamDTO)
        //{
        //    if (teamDTO == null)
        //    {
        //        return BadRequest();
        //    }

        //    var existingTeam = await _teamService.GetTeamByIdAsync(id);
        //    if (existingTeam == null)
        //    {
        //        return NotFound();
        //    }

        //    var team = _mapper.Map<Team>(teamDTO);

        //    await _teamService.UpdateTeamAsync(team);

        //    return Ok();
        //}
      

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
