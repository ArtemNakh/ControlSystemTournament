using AutoMapper;
using ControlSystemTournament.Core.Interfaces;
using ControlSystemTournament.Core.Models;
using ControlSystemTournament.Core.Services;
using ControlSystemTournament.DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.Collections.Generic;
using System.Data;

namespace ControlSystemTournament.Controllers
{
    [Route("api/PlayerRoles")]
    [ApiController]
    public class PlayerRoleController : ControllerBase
    {
        private readonly IPlayerRoleService _PlayerRoleService;
        private readonly IMapper _mapper;
        public PlayerRoleController(IPlayerRoleService playerRoleService, IMapper mapper)
        {
            _PlayerRoleService = playerRoleService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<PlayerRoleDTO>>> GetRoles([FromQuery] string? nameRole = null)
        {
            IEnumerable<PlayerRole> roles;

            if (!string.IsNullOrEmpty(nameRole))
            {
                var role = await _PlayerRoleService.GetPLayerRoleByNameAsync(nameRole);
                if (role == null || string.IsNullOrEmpty(role.Name))
                    return NotFound();

                roles = new List<PlayerRole> { role };
            }
            else
            {
                roles = await _PlayerRoleService.GetAllPLayerRolesAsync();
                if (roles == null || !roles.Any())
                    return NotFound();
            }


            var rolesDTO = _mapper.Map<IEnumerable<PlayerRoleDTO>>(roles);
            return Ok(rolesDTO);
        }


        [HttpGet("{id}")]
        public async Task<ActionResult<PlayerRoleDTO>> GetRoleById(int id)
        {
            var role = await _PlayerRoleService.GetPLayerRoleByIdAsync(id);
            if (role == null)
                return NotFound();

            var playerRoleDTO = _mapper.Map<PlayerRoleDTO>(role);
            return Ok(playerRoleDTO);
        }

        [HttpPost]
        public async Task<ActionResult<PlayerRoleDTO>> CreateRole([FromBody] string nameRole)
        {
            try
            {
                if(nameRole == null|| nameRole=="")
                    return BadRequest();
                var createdRole = await _PlayerRoleService.CreatePlayerRoleAsync(nameRole);
                var playerDTO = _mapper.Map<PlayerRoleDTO>(createdRole);
                return Created("Created player", playerDTO);
            }
            catch (Exception)
            {
                return BadRequest("Role with this name already exists");
                
            }

        }

        [HttpDelete]
        public async Task<ActionResult> DeleteRole(int id)
        {
            await _PlayerRoleService.DeletePlayerRoleAsync(id);
            return NoContent();
        }
    }
}
