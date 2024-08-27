using AutoMapper;
using ControlSystemTournament.Core.Interfaces;
using ControlSystemTournament.Core.Models;
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


        public PlayerController(IPlayerService playerService, IPlayerRoleService playerRoleService, IMapper mapper)
        {
            _playerService = playerService;
            _playerRoleService = playerRoleService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Player>>> GetPlayers([FromQuery] int? teamId,
                                                                        [FromQuery] int? tournamentId)
        {
            IEnumerable<Player> players;
            if (teamId != null)
                players = await _playerService.GetPlayersTeamAsync(teamId.Value);
            else if (tournamentId != null)
                players = await _playerService.GetPlayersTournamentAsync(tournamentId.Value);
            else
                return NoContent();

            if (players == null || players.IsNullOrEmpty())
                return NotFound();

            return Ok(players);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<PlayerDTO>> GetPlayer(int Id)
        {
            if (Id == null)
                return NotFound();
            var player = await _playerService.GetPlayerByIdAsync(Id);
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
                var player = _mapper.Map<Player>(playerDTO);
                await _playerService.CreatePlayerAsync(player);

                var createdPlayerDTO = _mapper.Map<PlayerDTO>(player);
                return Created("created player",createdPlayerDTO);
            }
            catch (Exception)
            {
                return BadRequest();
                throw;
            }
           
        }

        [HttpPut]
        public async Task<ActionResult<Player>> UpdatePlayer(PlayerDTO playerDTO)
        {
            try
            {
                var player = _mapper.Map<Player>(playerDTO);
                await _playerService.UpdatePlayerAsync(player);

                return Ok(player);
            }
            catch (Exception)
            {
                return NotFound();
                throw;
            }

        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeletePlayer(int id)
        {
            try
            {
                var player = _playerService.GetPlayerByIdAsync(id);
                if (player == null)
                    return BadRequest();

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
