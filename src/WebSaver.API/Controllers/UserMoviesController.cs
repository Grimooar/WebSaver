using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApplication1.Service;

namespace WebApplication1.Controllers;

/// <summary>
/// 
/// </summary>
[ApiController]
[Route("api/[controller]")]
[Authorize]
public class UserMoviesController : ControllerBase
{
    private readonly UserFavouriteService _userFavouriteService;

    /// <summary>
    /// 
    /// </summary>
    /// <param name="userFavouriteService"></param>
    public UserMoviesController(UserFavouriteService userFavouriteService)
    {
        this._userFavouriteService = userFavouriteService;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="userId"></param>
    /// <param name="movieId"></param>
    /// <returns></returns>
    [HttpPost("AddToFavorites")]
    public async Task<IActionResult> AddMovieToFavorites([FromQuery] int userId, [FromQuery] int movieId)
    {
        await _userFavouriteService.AddMovieToUserFavorites(userId, movieId);
        return Ok();
    }
   

    /// <summary>
    /// 
    /// </summary>
    /// <param name="userId"></param>
    /// <param name="movieId"></param>
    /// <returns></returns>
    [HttpPost("AddToViewed")]
    public async Task<IActionResult> AddMovieToViewed([FromQuery] int userId, [FromQuery] int movieId)
    {
        await _userFavouriteService.AddMovieToUserViewed(userId, movieId);
        return Ok();
    }
   
    /// <summary>
    /// 
    /// </summary>
    /// <param name="userId"></param>
    /// <param name="movieId"></param>
    /// <param name="newRating"></param>
    /// <returns></returns>
    [HttpPut("UpdateRating")]
    public async Task<IActionResult> UpdateMovieRating(int userId, int movieId, int newRating)
    {
        // Get the existing user-anime relationship
        var userAnime = await _userFavouriteService.GetMovieByUserId(userId, movieId);
        if (userAnime != null)
        {
            // Update the rating
            userAnime.Rating = newRating;

            // Update the user-anime relationship rating in the database
            await _userFavouriteService.UpdateUserMovieRating(userId, movieId, newRating);
        }
        else
        {
            await _userFavouriteService.UpdateUserMovieRating(userId, movieId, newRating);
        }

        return Ok();
    }
    /// <summary>
    /// 
    /// </summary>
    /// <param name="userId"></param>
    /// <param name="movieId"></param>
    /// <param name="newStatus"></param>
    /// <returns></returns>
    [HttpPut("UpdateStatus")]
    public async Task<IActionResult> UpdateMovieStatus([FromQuery] int userId, [FromQuery] int movieId, [FromQuery] int newStatus)
    {
        // Get the existing user-anime relationship
        var userMovie = await _userFavouriteService.GetMovieByUserId(userId, movieId);
        if (userMovie != null)
        {
            // Update the status
            userMovie.Status = newStatus;

            // Update the user-anime relationship in the database
            await _userFavouriteService.UpdateUserMovieStatus(userId,movieId,newStatus);
        }
        else
        {
            await _userFavouriteService.UpdateUserMovieStatus(userId, movieId, newStatus);
        }

        return Ok();
    }
   

    /// <summary>
    /// 
    /// </summary>
    /// <param name="userId"></param>
    /// <param name="movieId"></param>
    /// <returns></returns>
    [HttpPost("RemoveFromFavorites")]
    public async Task<IActionResult> RemoveMovieFromFavorites([FromQuery] int userId, [FromQuery] int movieId)
    {
        await _userFavouriteService.RemoveMovieFromUserFavorites(userId, movieId);
        return Ok();
    }
    
}