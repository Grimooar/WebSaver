using AutoMapper;
using Domain;
using DTOs;
using Kirel.Repositories.Interfaces;

namespace WebApplication1.Service
{
    public class SeriesService
    {
        private readonly IKirelGenericEntityFrameworkRepository<int, Series> _seriesRepository;
        private readonly IMapper _mapper;

        public SeriesService(IKirelGenericEntityFrameworkRepository<int, Series> seriesRepository, IMapper mapper)
        {
            _seriesRepository = seriesRepository;
            _mapper = mapper;
        }

        public async Task<List<SeriesDto>> GetMovieInfo(string movieTitle)
        {
            // Search the database for series with matching titles
            var existingMovie = await _seriesRepository.GetList(m => m.Name.Contains(movieTitle));

            if (!existingMovie.Any())
            {
                // If the series does not exist in the database, throw an exception
                throw new SeriesNotFoundException($"Series with the title '{movieTitle}' was not found in the database.");
            }

            // Map the list of Series entities to a list of SeriesDto using AutoMapper
            var movieList = _mapper.Map<List<SeriesDto>>(existingMovie);

            return movieList;
        }
        

        public class SeriesNotFoundException : Exception
        {
            public SeriesNotFoundException(string message) : base(message)
            {
            }
        }
    }
}