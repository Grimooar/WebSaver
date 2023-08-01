using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApplication1.Service;

namespace WebApplication1.Controllers;
[Authorize]
public class AnimeFavouriteController : ControllerBase
{
    
    private readonly UserFavouriteService _uAnimeService;

    public AnimeFavouriteController(UserFavouriteService uAnimeService)
    {
        _uAnimeService = uAnimeService;
    }
    
    
    [HttpPost("AddToFavorites")]
    public async Task<IActionResult> AddAnimeToFavorites([FromQuery] int userId, [FromQuery] int animeId)
    {
        await _uAnimeService.AddAnimeToUserFavorites(userId, animeId);
        return Ok();
    }
    
    [HttpPut("UpdateRating")]
    public async Task<IActionResult> UpdateAnimeRating([FromQuery] int userId, [FromQuery] int animeId, [FromQuery] int newRating)
    {
        // Get the existing user-anime relationship
        var userAnime = await _uAnimeService.GetUserAnimeByUserIdAndAnimeId(userId, animeId);
        if (userAnime != null)
        {
            // Update the rating
            userAnime.Rating = newRating;

            // Update the user-anime relationship rating in the database
            await _uAnimeService.UpdateUserAnimeRating(userId, animeId, newRating);
        }

        return Ok();
    }
    /*
   [HttpPost("RemoveFromFavorites")]
   public async Task<IActionResult> RemoveAnimeFromFavorites([FromQuery] int userId, [FromQuery] int animeId)
   {
       await userMoviesService.RemoveAnimeFromUserFavorites(userId, animeId);
       return Ok();
   }
   */
    [HttpPost("RemoveFromFavorites")]
    public async Task<IActionResult> RemoveMovieFromFavorites([FromQuery] int userId, [FromQuery] int animeId)
    {
        await _uAnimeService.RemoveAnimeFromUserFavorites(userId, animeId);
        return Ok();
    }
    [HttpPut("UpdateStatus")]
    public async Task<IActionResult> UpdateAnimeStatus([FromQuery] int userId, [FromQuery] int animeId, [FromQuery] int newStatus)
    {
        // Get the existing user-anime relationship
        var userAnime = await _uAnimeService.GetUserAnimeByUserIdAndAnimeId(userId, animeId);
        if (userAnime != null)
        {
            // Update the status
            userAnime.Status = newStatus;

            // Update the user-anime relationship in the database
            await _uAnimeService.UpdateUserAnimeStatus(userId,animeId,newStatus);
        }

        return Ok();
    }
}