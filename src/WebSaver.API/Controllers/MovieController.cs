
using DTOs;
using Microsoft.AspNetCore.Authorization;
using WebApplication1.Service;

namespace WebApplication1.Controllers;

using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

/// <summary>
/// 
/// </summary>
[ApiController]
[Route("api/movies")]
[Authorize]
public class MoviesController : ControllerBase
{
    private readonly MovieService _movieService;
    
    
    /// <summary>
    /// 
    /// </summary>
    /// <param name="movieService"></param>
    public MoviesController(MovieService movieService)
    {
        _movieService = movieService;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="movieTitle"></param>
    /// <returns></returns>
    [HttpGet("{movieTitle}")]
    public async Task<IActionResult> GetMovieInfo(string movieTitle)
    {
        // Вызов сервиса, чтобы получить информацию о фильме или аниме по названию
        List<MovieDto> movieInfo = await _movieService.GetMovieInfo(movieTitle);

        if (movieInfo == null)
        {
            // Возвращаем статус 404 Not Found, если фильм или аниме не найдены
            return NotFound();
        }

        // Возвращаем данные о фильме или аниме в формате JSON
        return Ok(movieInfo);
    }
}
