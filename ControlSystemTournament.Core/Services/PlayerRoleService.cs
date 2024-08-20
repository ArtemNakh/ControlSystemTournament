using ControlSystemTournament.Core.Interfaces;
using ControlSystemTournament.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControlSystemTournament.Core.Services
{
    public class PlayerRoleService : IPlayerRoleService
    {
        private readonly IRepository _context;

        public PlayerRoleService(IRepository context)
        {
            _context = context;
        }

        public async Task<PlayerRole> CreatePlayerRoleAsync(string nameRole)
        {
            if ((await _context.GetQuery<PlayerRole>(l => l.Name == nameRole)).FirstOrDefault() != null)
            {
                throw new Exception("Role with this name already exists.");
            }

            PlayerRole newPlayerRole = new PlayerRole()
            {
                Name = nameRole
            };

            await _context.Add(newPlayerRole);
           
            return newPlayerRole;
        }

        public async Task DeletePlayerRoleAsync(int id)
        {
            var match = await _context.GetById<PlayerRole>(id);
            if (match != null)
                await _context.Delete<PlayerRole>(id);

        }

        public Task<IEnumerable<PlayerRole>> GetAllPLayerRolesAsync()
        {
            var locations = (IEnumerable<PlayerRole>)_context.GetAll<PlayerRole>();
            return Task.FromResult(locations);
        }

        public Task<PlayerRole> GetPLayerRoleByIdAsync(int id)
        {
            return  _context.GetById<PlayerRole>(id);
        }

        public async Task<IEnumerable<PlayerRole>> GetPLayerRoleByNameAsync(string nameRole)
        {
            return await _context.GetQuery<PlayerRole>(n=>n.Name == nameRole);
        }
    

    }
}
