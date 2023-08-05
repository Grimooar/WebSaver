using DTOs;
using WebApplication1.Service;
using Microsoft.AspNetCore.Mvc;

namespace WebApplication1.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    [ApiController]
    [Route("api/movies")]
    public class CommentsController : ControllerBase
    {
        private readonly CommentsService _movieService;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="movieService"></param>
        public CommentsController(CommentsService movieService)
        {
            _movieService = movieService;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="movieId"></param>
        /// <returns></returns>
        [HttpGet("{movieId}/comments")]
        public async Task<IActionResult> GetMovieComments(int movieId)
        {
            var movieComments = await _movieService.GetMovieComments(movieId);
            return Ok(movieComments);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="movieId"></param>
        /// <returns></returns>
        [HttpGet("{userId}/{movieId}/user-comments")]
        public async Task<IActionResult> GetUserMovieComments(int userId, int movieId)
        {
            var userMovieComments = await _movieService.GetUserMovieComments(userId, movieId);
            return Ok(userMovieComments);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="commentDto"></param>
        /// <returns></returns>
        [HttpPost("comments")]
        public async Task<IActionResult> AddMovieComment([FromBody] CommentCreateDto commentDto)
        {
            await _movieService.AddMovieComment(commentDto);
            return Ok();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="commentId"></param>
        /// <param name="newCommentText"></param>
        /// <returns></returns>
        [HttpPut("comments/{commentId}")]
        public async Task<IActionResult> UpdateMovieComment(int commentId, [FromBody] string newCommentText)
        {
            await _movieService.UpdateMovieComment(commentId, newCommentText);
            return Ok();
        }
    }
}
