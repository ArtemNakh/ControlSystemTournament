using AutoMapper;
using ControlSystemTournament.Core.Interfaces;
using ControlSystemTournament.Core.Models;
using ControlSystemTournament.Core.Services;
using ControlSystemTournament.DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.Collections.Generic;

namespace ControlSystemTournament.Controllers
{

    [ApiController]
    [Route("api/Player")]
    public class PlayerController : ControllerBase
    {
        private readonly IPlayerService _playerService;
        private readonly ITeamService _teamService;
        private readonly IPlayerRoleService _playerRoleService;
        private readonly IMapper _mapper;


        public PlayerController(IPlayerService playerService, IPlayerRoleService playerRoleService, IMapper mapper, ITeamService teamService)
        {
            _playerService = playerService;
            _playerRoleService = playerRoleService;
            _mapper = mapper;
            _teamService = teamService;
        }

        [HttpGet("tournament/{tournamentId}")]
        public async Task<ActionResult<IEnumerable<PlayerDTO>>> GetPlayersByTournament( int tournamentId)
        {
            IEnumerable<Player> players = await _playerService.GetPlayersTournamentAsync(tournamentId);

            if (players == null || !players.Any())
                return NotFound();

            var playerDTOs = _mapper.Map<IEnumerable<PlayerDTO>>(players);
            return Ok(playerDTOs);


        }

        [HttpGet("team/{teamId}")]
        public async Task<ActionResult<IEnumerable<PlayerDTO>>> GetPlayersByTeam( int teamId)
        {
            IEnumerable<Player> players = await _playerService.GetPlayersTeamAsync(teamId);

            if (players == null || !players.Any())
                return NotFound();

            var playerDTOs = _mapper.Map<IEnumerable<PlayerDTO>>(players);
            return Ok(playerDTOs);


        }

        [HttpGet("{id}")]
        public async Task<ActionResult<PlayerDTO>> GetPlayer(int id)
        {
            if (id == null)
                return NotFound();
            var player = await _playerService.GetPlayerByIdAsync(id);
            if (player == null)
                return NotFound();

            var playerDTO = _mapper.Map<PlayerDTO>(player);
            return Ok(playerDTO);
        }


        //todo DTO
        [HttpPost]
        public async Task<ActionResult<PlayerDTO>> CreatePlayer([FromBody] PlayerDTO playerDTO)
        {
            if (playerDTO == null)
                return BadRequest();

           
            try
            {

                Team team = _teamService.GetTeamByIdAsync(playerDTO.TeamId).Result;
                if (team == null)
                    return NotFound("Team not found");

                if( _playerService.GetPlayersTournamentAsync(team.Tournament.Id).Result.Where(n => n.FirstName == playerDTO.FirstName && n.LastName == playerDTO.LastName && n.Team.Tournament.Id==team.Tournament.Id ).Count()>0)
                    return BadRequest("this player already play");

                PlayerRole role = _playerRoleService.GetPLayerRoleByIdAsync(playerDTO.RoleId).Result;

                if ( role == null)
                    return BadRequest("Role mot found");

                var player = _mapper.Map<Player>(playerDTO);
                player.Team = team;
                player.Role = role;

                await _playerService.CreatePlayerAsync(player);


                team.Players.Add(player);
                _teamService.UpdateTeamAsync(team);

                var createdPlayerDTO = _mapper.Map<PlayerDTO>(player);
                return Created("created player", createdPlayerDTO);
            }
            catch (Exception)
            {
                return BadRequest();
            }

        }

        //[HttpPut]
        //public async Task<ActionResult<Player>> UpdatePlayer(PlayerDTO playerDTO)
        //{
        //    try
        //    {
        //        var player = _mapper.Map<Player>(playerDTO);
        //        await _playerService.UpdatePlayerAsync(player);

        //        return Ok(player);
        //    }
        //    catch (Exception)
        //    {
        //        return NotFound();
        //        throw;
        //    }

        //}

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeletePlayer(int id)
        {
            try
            {
                Player player = await _playerService.GetPlayerByIdAsync(id);
                if (player == null)
                    return BadRequest();

                Team team = await _teamService.GetTeamByIdAsync(player.Team.Id);
                if (team.Coach!=null && team.Coach.Id == player.Id)
                {
                    team.Coach = null;
                    _teamService.UpdateTeamAsync(team);
                }
                _playerService.DeletePlayerAsync(id);
                return Ok();

            }
            catch (Exception)
            {
                return NotFound();
                throw;
            }
        }

    }
}
