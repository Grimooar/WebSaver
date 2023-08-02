using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApplication1.Service;

namespace WebApplication1.Controllers;

/// <summary>
/// Controller for managing favorite anime of users.
/// </summary>
[ApiController]
[Authorize]
[Route("api/AU")]
public class AnimeFavouriteController : ControllerBase
{
    
    private readonly UserFavouriteService _uAnimeService;

    public AnimeFavouriteController(UserFavouriteService uAnimeService)
    {
        _uAnimeService = uAnimeService;
    }
    
    
    [HttpPost("AddToFavorites")]

    [HttpPost("AF")]
    /// <summary>
    /// Adds anime to the list of favorites for the specified user.
    /// </summary>
    /// <param name="userId">User ID.</param>
    /// <param name="animeId">Anime ID.</param>
    /// <returns>Returns a successful result of the operation.</returns>
    public async Task<IActionResult> AddAnimeToFavorites([FromQuery] int userId, [FromQuery] int animeId)
    {
        await _uAnimeService.AddAnimeToUserFavorites(userId, animeId);
        return Ok();
    }

    /// <summary>
    /// Updates the anime rating for the specified user.
    /// </summary>
    /// <param name="userId">User ID.</param>
    /// <param name="animeId">Anime ID.</param>
    /// <param name="newRating">New anime rating.</param>
    /// <returns>Returns a successful result of the operation.</returns>
    [HttpPut(nameof(UpdateAnimeRating))]

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

    [HttpPost("RemoveMovieFromFavorites")]

    public async Task<IActionResult> RemoveMovieFromFavorites([FromQuery] int userId, [FromQuery] int animeId)
    {
        await _uAnimeService.RemoveAnimeFromUserFavorites(userId, animeId);
        return Ok();
    }

    [HttpPut(nameof(UpdateAnimeStatus))]

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