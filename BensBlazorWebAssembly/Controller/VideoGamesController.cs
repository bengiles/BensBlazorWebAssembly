using BensBlazorWebAssembly.Common.Entities;
using BensBlazorWebAssembly.Data;
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

    }
}
