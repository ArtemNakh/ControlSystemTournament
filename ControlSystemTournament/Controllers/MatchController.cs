using AutoMapper;
using ControlSystemTournament.Core.Interfaces;
using ControlSystemTournament.Core.Models;
using ControlSystemTournament.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace ControlSystemTournament.Controllers
{

    [ApiController]
    [Route("api/Match")]
    public class MatchController : ControllerBase
    {
        private readonly IMatchService _matchService;
        private readonly IMapper _mapper;
        private readonly ITeamService _teamService;
        private readonly ITournamentService _tournamentService;


        public MatchController(IMatchService matchService, IMapper mapper, ITeamService teamService, ITournamentService tournamentService)
        {
            _matchService = matchService;
            _mapper = mapper;
            _teamService = teamService;
            _tournamentService = tournamentService;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<MatchDTO>> GetMatchById(int id)
        {
            var match = await _matchService.GetMatchByIdAsync(id);
            if (match == null)
            {
                return NotFound();
            }
            var matchDTO = _mapper.Map<MatchDTO>(match);
            return Ok(matchDTO);
        }

        [HttpPost]
        public async Task<ActionResult<MatchDTO>> CreateMatch([FromBody] MatchDTO matchDTO)
        {
            if (matchDTO == null || matchDTO.TeamAId == matchDTO.TeamBId)
            {
                return BadRequest();
            }

            var match = _mapper.Map<Match>(matchDTO);

            try
            {
                var tournament = await _tournamentService.GetTournamentByIdAsync(matchDTO.TournamentId);
                var teamA = await _teamService.GetTeamByIdAsync(matchDTO.TeamAId);
                var teamB = await _teamService.GetTeamByIdAsync(matchDTO.TeamBId);

                if (tournament == null)
                    return BadRequest("tournament not found");
                if (teamA == null || teamB == null)
                    return BadRequest("team not found.Please check correct value");

                match.Tournament = tournament;
                match.TeamA = teamA;
                match.TeamB = teamB;
                match.WinnerTeam = null;
                if (string.IsNullOrWhiteSpace(match.TeamB.Country) || string.IsNullOrWhiteSpace(match.TeamB.Country) ||
                                        string.IsNullOrEmpty(match.TeamB.Country) || string.IsNullOrEmpty(match.TeamB.Country))
                {
                    throw new ArgumentException("Country cannot be null or empty.");
                }


                await _matchService.CreateMatchAsync(match);

                var createdMatchDTO = _mapper.Map<MatchDTO>(match);
                return Created("Created Match", createdMatchDTO);
            }
            catch (Exception)
            {
                throw new ArgumentException("error: does not succesfull adding match ");
            }


        }

        [HttpPut("{idMatch}")]
        public async Task<ActionResult> UpdateMatch(int idMatch, int ScoreTeamA, int ScoreTeamB, int WinnerTeamId)
        {
            if (ScoreTeamA == null || ScoreTeamB == null || WinnerTeamId == null)
            {
                return BadRequest();
            }

            var existingMatch = await _matchService.GetMatchByIdAsync(idMatch);
            if (existingMatch == null)
            {
                return NotFound();
            }
            var winnerTeam = await _teamService.GetTeamByIdAsync(WinnerTeamId);
            if (WinnerTeamId == existingMatch.TeamB.Id || WinnerTeamId == existingMatch.TeamA.Id)
            {
                existingMatch.WinnerTeam = winnerTeam;
                existingMatch.ScoreTeamB = ScoreTeamB;
                existingMatch.ScoreTeamA = ScoreTeamA;
                await _matchService.UpdateMatchAsync(existingMatch);
                return Ok();
            }
            else
            {
                return BadRequest("enter the correct team");
            }
        }


        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteMatch(int id)
        {
            var match = await _matchService.GetMatchByIdAsync(id);
            if (match == null)
            {
                return NotFound();
            }

            await _matchService.DeleteMatchAsync(id);
            return NoContent();
        }


        [HttpGet("tournament/{tournamentId}")]
        public async Task<ActionResult<IEnumerable<Match>>> GetMatchesByTournament(int tournamentId)
        {
            try
            {
                var tournament = await _tournamentService.GetTournamentByIdAsync(tournamentId);
                if (tournament == null)
                    return NotFound("tournament not found");
                var matches = await _matchService.GetAllMatchesTournamentAsync(tournament);

                var matchList = matches.ToList();

                var matchesDTO = _mapper.Map<IEnumerable<MatchDTO>>(matchList);

                return Ok(matchesDTO);
            }
            catch (Exception)
            {

                throw new Exception("Incorrect Id tournament");
            }

        }

    }
}
