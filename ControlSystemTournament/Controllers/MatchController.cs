using AutoMapper;
using ControlSystemTournament.Core.Interfaces;
using ControlSystemTournament.Core.Models;
using ControlSystemTournament.DTOs;
using Microsoft.AspNetCore.Http;
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
            if (matchDTO == null|| matchDTO.TeamAId==matchDTO.TeamBId)
            {
                return BadRequest();
            }

            var match = _mapper.Map<Match>(matchDTO);

            try
            {
                match.Tournament = await _tournamentService.GetTournamentByIdAsync(matchDTO.TournamentId);
                match.TeamA = await _teamService.GetTeamByIdAsync(matchDTO.TeamAId);
                match.TeamB = await _teamService.GetTeamByIdAsync(matchDTO.TeamBId);
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

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateMatch(int id,  int ScoreTeamA,  int ScoreTeamB, int WinnerTeamId)
        {
            if (ScoreTeamA == null|| ScoreTeamB==null||WinnerTeamId==null)
            {
                return BadRequest();
            }

            var existingMatch = await _matchService.GetMatchByIdAsync(id);
            if (existingMatch == null)
            {
                return NotFound();
            }

             existingMatch.WinnerTeam = _teamService.GetTeamByIdAsync(WinnerTeamId).Result;
            existingMatch.ScoreTeamB = ScoreTeamB;
            existingMatch.ScoreTeamA = ScoreTeamA;
            await _matchService.UpdateMatchAsync(existingMatch);
            return Ok() ;
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
