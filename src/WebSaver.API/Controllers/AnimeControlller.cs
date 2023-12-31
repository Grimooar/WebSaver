using WebApplication1.Service;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Domain;
using Microsoft.AspNetCore.Authorization;
using Swashbuckle.AspNetCore.Annotations;


namespace WebApplication1.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    [ApiController]
    [Authorize]

    [Route("api/[controller]")]
 
    public class AnimeController : ControllerBase
    {
        private readonly MyAnimeListService _animeService;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="animeService"></param>
        public AnimeController(MyAnimeListService animeService)
        {
            _animeService = animeService;
        }


        /// <summary>
        /// Get list of anime by name 
        /// </summary>
        /// <param name="animeName">Anime name for serach.</param>
        /// <returns>list of anime that contains provided name .</returns>
        [HttpGet]
        [ProducesResponseType(typeof(List<Anime>), 200)]
        [ProducesResponseType(typeof(string), 400)]
        [SwaggerOperation(Summary = "Получение списка аниме по названию.")]

        public async Task<IActionResult> GetAnime(string animeName)
        {
            if (string.IsNullOrWhiteSpace(animeName))
                return BadRequest("Anime name cannot be empty.");

            var animeList = await _animeService.SearchAnime(animeName);

            return Ok(animeList);
        }
    }

}
