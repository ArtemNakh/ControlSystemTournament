using ControlSystemTournament.Core.Interfaces;
using ControlSystemTournament.Core.Models;
using ControlSystemTournament.Core.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.Collections.Generic;

namespace ControlSystemTournament.Controllers
{
    [Route("api/PlayerRoles")]
    [ApiController]
    public class PlayerRoleController : ControllerBase
    {
        private readonly IPlayerRoleService _PlayerRoleService;

        public PlayerRoleController(IPlayerRoleService playerRoleService)
        {
            _PlayerRoleService = playerRoleService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<PlayerRole>>> GetAllRoles([FromQuery] string? nameRole)
        {
            IEnumerable<PlayerRole> roles = null;
            if (nameRole != null)
                roles = await _PlayerRoleService.GetPLayerRoleByNameAsync(nameRole);
            else
                roles = await _PlayerRoleService.GetAllPLayerRolesAsync();

            if (roles == null|| roles.IsNullOrEmpty())
                return NotFound();

            return Ok(roles);
        }


        [HttpGet("{id}")]
        public async Task<ActionResult<PlayerRole>> GetRoleById(int id)
        {
            var location = await _PlayerRoleService.GetPLayerRoleByIdAsync(id);
            if (location == null)
                return NotFound();

            return Ok(location);
        }
        

        [HttpPost]
        public async Task<ActionResult<PlayerRole>> CreateRole([FromBody] string nameRole)
        {
            try
            {
                var createdRole = await _PlayerRoleService.CreatePlayerRoleAsync(nameRole);
                return Created();
            }
            catch (Exception)
            {
                return BadRequest("Role with this name already exists");
                throw;
            }

        }

        [HttpDelete]
        public async Task<IActionResult> DeleteRole(int id)
        {

            await _PlayerRoleService.DeletePlayerRoleAsync(id);
            return NoContent();
        }
    }
}
