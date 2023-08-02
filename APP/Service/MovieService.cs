using System.Text.Json;
using AutoMapper;
using DTOs;
using Kirel.Repositories.Infrastructure.Generics;
using Kirel.Repositories.Interfaces;
using WebApplication1.DTOs;
using WebApplication1.Models;

namespace WebApplication1.Service;

public class MovieService
{
    
    private readonly IKirelGenericEntityFrameworkRepository<int, Movie> movieRepository; 
    private readonly IMapper _mapper;

    public MovieService(IKirelGenericEntityFrameworkRepository<int, Movie> movieRepository, IMapper mapper)
    {
        this.movieRepository = movieRepository;
        _mapper = mapper;
    }

    public async Task<List<MovieDto>> GetMovieInfo(string movieTitle)
    {

        // Search the database for animes with matching titles
        var existingMovie = await movieRepository.GetList(m => m.Title.Contains(movieTitle));

        List<MovieDto> movieList = new List<MovieDto>();

        if (existingMovie.Any())
        {
            // If animes with the same title exist in the database, add them to the list
            foreach (var movie in existingMovie)
            {
                MovieDto dto = new MovieDto
                {
                    Id = movie.Id,
                    Year = movie.Year,
                    Title = movie.Title,
                    Rating = movie.Rating,
                    Description = movie.Description,
                    Poster = movie.Poster
                };

                movieList.Add(dto);
            }
        }
        else
        {
            // If the anime does not exist in the database, throw an exception
            throw new MovieNotFoundException($"Movie with the title '{movieTitle}' was not found in the database.");
        }

        return movieList;
    }
    public class MovieNotFoundException : Exception
    {
        public MovieNotFoundException(string message) : base(message)
        {
        }
    }
}
    
