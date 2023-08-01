using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApplication1.Service;

namespace WebApplication1.Controllers;
[Authorize]
public class UserSeriesController : ControllerBase
{
     private readonly UserSeriesService _uSeriesService;

    public UserSeriesController(UserSeriesService uSeriesService)
    {
        this._uSeriesService = uSeriesService;
    }
    
    
    [HttpPost("AddSeriesToFavorites")]
    public async Task<IActionResult> AddSeriesToFavorites([FromQuery] int userId, [FromQuery] int animeId)
    {
        await _uSeriesService.AddSeriesToUserFavorites(userId, animeId);
        return Ok();
    }
    
    [HttpPut("UpdateSeriesRating")]
    public async Task<IActionResult> UpdateSeriesRating([FromQuery] int userId, [FromQuery] int seriesId, [FromQuery] int newRating)
    {
        // Get the existing user-anime relationship
        var userAnime = await _uSeriesService.GetUserSeriesByUserIdAndSeriesId(userId, seriesId);
        if (userAnime != null)
        {
            // Update the rating
            userAnime.Rating = newRating;

            // Update the user-anime relationship rating in the database
            await _uSeriesService.UpdateUserSeriesRating(userId, seriesId, newRating);
        }
        else
        {
            await _uSeriesService.UpdateUserSeriesRating(userId, seriesId, newRating);
        }

        return Ok();
    }
    
   
    /*[HttpPost("RemoveFromFavorites")]
    public async Task<IActionResult> RemoveMovieFromFavorites([FromQuery] int userId, [FromQuery] int animeId)
    {
        await _uSeriesService.RemoveAnimeFromUserFavorites(userId, animeId);
        return Ok();
    }*/
    [HttpPut("UpdateSeriesStatus")]
    public async Task<IActionResult> UpdateSeriesStatus([FromQuery] int userId, [FromQuery] int seriesId, [FromQuery] int newStatus)
    {
        // Get the existing user-anime relationship
        var userAnime = await _uSeriesService.GetUserSeriesByUserIdAndSeriesId(userId, seriesId);
        if (userAnime != null)
        {
            // Update the status
            userAnime.Status = newStatus;

            // Update the user-anime relationship in the database
            await _uSeriesService.UpdateUserSeriesStatus(userId,seriesId,newStatus);
        }
        else
        {
            await _uSeriesService.UpdateUserSeriesStatus(userId,seriesId,newStatus);
        }

        return Ok();
    }
}