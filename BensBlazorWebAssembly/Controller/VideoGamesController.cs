using BensBlazorWebAssembly.Common.Entities;
using BensBlazorWebAssembly.Data;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BensBlazorWebAssembly.Controller
{
    
    [Route("api/v{version:apiVersion}/VideoGames")]
    [ApiVersion("1.0")]
    [ApiController]

    public class VideoGamesController : ControllerBase
    {
       private readonly Context _context;

        public VideoGamesController(Context context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<VideoGame>>> GetVideoGamesAsync()
        {
            var response =  await _context.Games.ToListAsync();
            return response;
        }

        [ApiVersion("2.0")]
        [HttpGet]
        public IActionResult GetVideoGamesAsync_V2()
        {
            // just for fun lets create a v2 of the API that returns the games in reverse order as an IActionResult
            var response = _context.Games.OrderByDescending(x => x.Id).ToList();
            return Ok(response);
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<ActionResult<VideoGame>> GetVideoGameAsync(int id)
        {
            var response = await _context.Games.FindAsync(id);
            if (response == null)
            {
                return NotFound();
            }
            return response;
        }

        [HttpPost("new")]
        public async Task<IActionResult> AddVideoGameAsync([FromBody] VideoGame videoGame)
        {
            try
            {
                _context.Games.Add(videoGame);
                await _context.SaveChangesAsync();
                // get new id
                var newId = CreatedAtAction(nameof(GetVideoGameAsync), new { id = videoGame.Id }, videoGame)
                    .RouteValues.Values.FirstOrDefault();
                var newlyAddedVideogame = await _context.Games.FindAsync(newId);
                return Ok(newlyAddedVideogame);
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> DeleteVideoGameAsync(int id)
        {
            var videoGame = await _context.Games.FindAsync(id);
            if (videoGame == null)
            {
                return NotFound();
            }

            _context.Games.Remove(videoGame);
            await _context.SaveChangesAsync();

            return NoContent();
        }

    }
}
