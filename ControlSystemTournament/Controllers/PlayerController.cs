//using ControlSystemTournament.Core.Interfaces;
//using ControlSystemTournament.Core.Models;
//using Microsoft.AspNetCore.Http;
//using Microsoft.AspNetCore.Mvc;

//namespace ControlSystemTournament.Controllers
//{
   
//    [ApiController]
//    [Route("api/[Player]")]
//    public class PlayerController : ControllerBase
//    {
//        private readonly IPlayerService _playerService;

//        public PlayerController(IPlayerService playerService)
//        {
//            _playerService = playerService;
//        }

//        //[HttpGet]
//        //public async Task<ActionResult<IEnumerable<Player>>> GetPlayers()
//        //{
//        //    var players = await _playerService. .GetAllPlayersAsync();
//        //    return Ok(players);
//        //}

//        [HttpGet("{id}")]
//        public async Task<ActionResult<Player>> GetPlayer(int id)
//        {
//            var player = await _playerService.GetPlayerByIdAsync(id);
//            if (player == null)
//            {
//                return NotFound();
//            }
//            return Ok(player);
//        }

//        [HttpPost]
//        public async Task<ActionResult<Player>> CreatePlayer(string nickname,string firstName,string lastName,
//            int age,string country)
//        {
//            Player newPlayer=new Player()
//            {
//                Nickname=name,
//                FirstName="test",
//                LastName="lastname",

//            }
//            var createdPlayer = await _playerService.CreatePlayerAsync(player);
//            return CreatedAtAction(nameof(GetPlayer), new { id = createdPlayer.Id }, createdPlayer);
//        }

//        [HttpPut("{id}")]
//        public async Task<IActionResult> UpdatePlayer(int id, Player player)
//        {
//            if (id != player.Id)
//            {
//                return BadRequest();
//            }

//            await _playerService.UpdatePlayerAsync(player);
//            return NoContent();
//        }

//        [HttpDelete("{id}")]
//        public async Task<IActionResult> DeletePlayer(int id)
//        {
//            await _playerService.DeletePlayerAsync(id);
//            return NoContent();
//        }
//    }
//}
