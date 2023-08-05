using AutoMapper;
using Domain;
using DTOs;
using Kirel.Repositories.Interfaces;

namespace WebApplication1.Service;

public class CommentsService
{
    private readonly IMapper _mapper;
    private readonly IKirelGenericEntityFrameworkRepository<int, MovieComment> _movieCommentRepository;
    
    public CommentsService(IKirelGenericEntityFrameworkRepository<int, MovieComment> movieCommentRepository,
        IMapper mapper)
    {
        _movieCommentRepository = movieCommentRepository;
        _mapper = mapper;
    }
    public async Task<List<CommentDto>> GetMovieComments(int movieId)
    {
        // Search the database for comments associated with the specified movie
        var movieComments = await _movieCommentRepository.GetList(c => c.MovieId == movieId);

        // Map MovieComment entities to MovieCommentDto objects
        var movieCommentDtos = _mapper.Map<List<CommentDto>>(movieComments);

        return movieCommentDtos;
    }
    public async Task UpdateMovieComment(int commentId, string newCommentText)
    {
        // Получаем комментарий по его ID
        var existingComment = await _movieCommentRepository.GetById(commentId);

        if (existingComment == null)
        {
            throw new CommentNotFoundException($"Comment with ID {commentId} was not found in the database.");
        }

        // Обновляем текст комментария
        existingComment.Comment = newCommentText;

        // Сохраняем изменения в базе данных
        await _movieCommentRepository.Update(existingComment);
    }

    public async Task AddMovieComment(CommentCreateDto commentDto)
    {
        // Map MovieCommentDto to MovieComment entity
        var movieComment = _mapper.Map<MovieComment>(commentDto);

        // Save the new comment in the database
        await _movieCommentRepository.Insert(movieComment);
    }
    public async Task<List<CommentDto>> GetUserMovieComments(int userId, int movieId)
    {
        // Search the database for comments associated with the specified userId and movieId
        var movieComments = await _movieCommentRepository.GetList(c => c.UserId == userId && c.MovieId == movieId);

        // Map MovieComment entities to MovieCommentDto objects
        var movieCommentDtos = _mapper.Map<List<CommentDto>>(movieComments);

        return movieCommentDtos;
    }
    public class CommentNotFoundException : Exception
    {
        public CommentNotFoundException(string message) : base(message)
        {
        }
    }
}
