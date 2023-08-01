using WebApplication1.Service;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;

namespace WebApplication1.Controllers
{
    [ApiController]
    [Authorize]
    [Route("api/[controller]")]
    public class AnimeController : ControllerBase
    {
        private readonly MyAnimeListService _animeService;

        public AnimeController(MyAnimeListService animeService)
        {
            _animeService = animeService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAnime(string animeName)
        {
            if (string.IsNullOrWhiteSpace(animeName))
                return BadRequest("Anime name cannot be empty.");

            var animeList = await _animeService.SearchAnime(animeName);

            return Ok(animeList);
        }
    }
}
