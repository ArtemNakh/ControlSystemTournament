using ControlSystemTournament.Core.Interfaces;
using ControlSystemTournament.Core.Models;
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


        public PlayerController(IPlayerService playerService, IPlayerRoleService playerRoleService)
        {
            _playerService = playerService;
            _playerRoleService = playerRoleService;
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
        public async Task<ActionResult<Player>> GetPlayer(int Id)
        {
            if (Id == null)
                return NotFound();
            var player = await _playerService.GetPlayerByIdAsync(Id);
            if (player == null)
                return NotFound();

            return Ok(player);
        }


        //todo DTO
        [HttpPost]
        public async Task<ActionResult<Player>> CreatePlayer([FromBody]Player player)
        {
           if (player ==null)
                return BadRequest();

            try
            {
                _playerService.CreatePlayerAsync(player);
                return Ok(player);
            }
            catch (Exception)
            {
                return BadRequest();
                throw;
            }
           
        }

        [HttpPut]
        public async Task<ActionResult<Player>> UpdatePlayer(Player player)
        {
            try
            {
                _playerService.UpdatePlayerAsync(player);
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
