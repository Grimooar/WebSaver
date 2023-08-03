using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using WebApplication1.Service;

namespace WebApplication1.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    [ApiController]
    [Authorize]
    [Route("api/[controller]")]
    public class SeriesController : ControllerBase
    {
        private readonly SeriesService _seriesService;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="seriesService"></param>
        public SeriesController(SeriesService seriesService)
        {
            _seriesService = seriesService;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="movieTitle"></param>
        /// <returns></returns>
        [HttpGet("{movieTitle}")]
        public async Task<IActionResult> GetSeriesInfo(string movieTitle)
        {
            try
            {
                var seriesList = await _seriesService.GetMovieInfo(movieTitle);
                return Ok(seriesList);
            }
            catch (SeriesService.SeriesNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                // Log the exception for debugging purposes
                // Return a generic error message to the client
                return StatusCode(500, "An error occurred while processing the request.");
            }
        }
    }
}