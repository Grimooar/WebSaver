using System.Linq.Expressions;
using WebApplication1.Models;

namespace WebApplication1.Service;

using System.Threading.Tasks;
using Kirel.Repositories.Interfaces;


public class UserFavouriteService
{
    private readonly IKirelGenericEntityFrameworkRepository<int, UserMovies> userMoviesRepository;
    private readonly IKirelGenericEntityFrameworkRepository<int, UserAnime> userAnimeRepository;

    public UserFavouriteService(IKirelGenericEntityFrameworkRepository<int, UserMovies> userMoviesRepository,
        IKirelGenericEntityFrameworkRepository<int, UserAnime> userAnimeRepository)
    {
        this.userMoviesRepository = userMoviesRepository;
        this.userAnimeRepository = userAnimeRepository;
    }

    public async Task UpdateUserAnimeRating(int userId, int animeId, int newRating)
    {
        // Check if the user-anime relation already exists
        var existingRelation = await userAnimeRepository.GetList(ua => ua.UserId == userId && ua.AnimeId == animeId);
        if (existingRelation.Any())
        {
            // The relation already exists, update the rating
            var userAnime = existingRelation.FirstOrDefault();
            userAnime.Rating = newRating;
            await userAnimeRepository.Update(userAnime);
        }
        else
        {
            // The relation does not exist, so create a new one and add it to the database
            var userAnime = new UserAnime
            {
                UserId = userId,
                AnimeId = animeId,
                Rating = newRating
            };
            await userAnimeRepository.Insert(userAnime);
        }
    }

    public async Task<UserAnime> GetUserAnimeByUserIdAndAnimeId(int userId, int animeId)
    {
        // Filter by UserId and AnimeId
        Expression<Func<UserAnime, bool>> filter = ua => ua.UserId == userId && ua.AnimeId == animeId;

        // Get the first matching record
        var userAnimeList = await userAnimeRepository.GetList(filter);
        var userAnime = userAnimeList.FirstOrDefault();

        return userAnime;
    }
    public async Task<UserMovies> GetMovieByUserId(int userId, int movieId)
    {
        // Filter by UserId and AnimeId
        Expression<Func<UserMovies, bool>> filter = ua => ua.UserId == userId && ua.MovieId == movieId;

        // Get the first matching record
        var userMovieList = await userMoviesRepository.GetList(filter);
        var userMovie = userMovieList.FirstOrDefault();

        return userMovie;
    }
    public async Task UpdateUserAnimeStatus(int userId, int animeId, int newStatus)
    {
        // Check if the user-anime relation already exists
        var existingRelation = await userAnimeRepository.GetList(ua => ua.UserId == userId && ua.AnimeId == animeId);
        if (existingRelation.Any())
        {
            // The relation already exists, update the rating
            var userAnime = existingRelation.FirstOrDefault();
            userAnime.Status = newStatus;
            await userAnimeRepository.Update(userAnime);
        }
        else
        {
            // The relation does not exist, so create a new one and add it to the database
            var userAnime = new UserAnime
            {
                UserId = userId,
                AnimeId = animeId,
                Status = newStatus
            };
            await userAnimeRepository.Insert(userAnime);
        }
    }
    public async Task UpdateUserMovieStatus(int userId, int movieId, int newStatus)
    {
        
        var existingRelation = await userMoviesRepository.GetList(um => um.UserId == userId && um.MovieId == movieId);
        if (existingRelation.Any())
        {
            // The relation already exists, update the rating
            var userMovie = existingRelation.FirstOrDefault();
            userMovie.Status = newStatus;
            await userMoviesRepository.Update(userMovie);
        }
        else
        {
            // The relation does not exist, so create a new one and add it to the database
            var userMovie = new UserMovies
            {
                UserId = userId,
                MovieId = movieId,
                Status = newStatus
            };
            await userMoviesRepository.Insert(userMovie);
        }
    }
    public async Task UpdateUserMovieRating(int userId, int movieId, int newRating)
    {
        // Check if the user-movie relation already exists
        var existingRelation = await userMoviesRepository.GetList(um => um.UserId == userId && um.MovieId == movieId);
        if (existingRelation.Any())
        {
            // The relation already exists, update the rating
            var userMovie = existingRelation.FirstOrDefault();
            userMovie.Rating = newRating;
            await userMoviesRepository.Update(userMovie);
        }
        else
        {
            // The relation does not exist, so create a new one and add it to the database
            var userMovie = new UserMovies
            {
                UserId = userId,
                MovieId = movieId,
                Rating = newRating
            };
            await userMoviesRepository.Insert(userMovie);
        }
    }


    public async Task AddMovieToUserFavorites(int userId, int movieId)
    {
        // Check if the user-movie relation already exists
        var existingRelation = await userMoviesRepository.GetList(um => um.UserId == userId && um.MovieId == movieId);

        if (existingRelation.Count() == 0)
        {
            // The relation does not exist, so create a new one and add it to the database
            var userMovie = new UserMovies
            {
                UserId = userId,
                FavouriteStatus = true,
                MovieId = movieId,
            };

            await userMoviesRepository.Insert(userMovie);
        }
        else
        {
            // The relation already exists, update the FavouriteStatus of the existing entity
            var userMovie = existingRelation.First(); // Get the first existing entity

            userMovie.FavouriteStatus = true; // Update the FavouriteStatus

            await userMoviesRepository.Update(userMovie);
        }
    }
    
    public async Task RemoveMovieFromUserFavorites(int userId, int movieId)
    {
        // Check if the user-movie relation exists
        var existingRelation = await userMoviesRepository.GetList(um => um.UserId == userId && um.MovieId == movieId);
        if (existingRelation.Count() > 0)
        {

            var userMovie = existingRelation.First(); // Get the first existing entity

            userMovie.FavouriteStatus = false; // Update the FavouriteStatus

            await userMoviesRepository.Update(userMovie);
        }
           
        }
        // else: The relation does not exist, do nothing

        public async Task AddAnimeToUserFavorites(int userId, int animeId)
    {
        // Check if the user-anime relation already exists
        var existingRelation = await userAnimeRepository.GetList(ua => ua.UserId == userId && ua.AnimeId == animeId);
        if (existingRelation.Count() == 0)
        {
            // The relation does not exist, so create a new one and add it to the database
            var userAnime = new UserAnime
            {
                UserId = userId,
                FavouriteStatus = true,
                AnimeId = animeId
            };
            await userAnimeRepository.Insert(userAnime);
        }
        else
        {
            var userAnime = existingRelation.First(); // Get the first existing entity

            userAnime.FavouriteStatus = true; // Update the FavouriteStatus

            await userAnimeRepository.Update(userAnime);
        }

        // Add other methods for managing the UserMovies entity as needed
    }
    
    public async Task RemoveAnimeFromUserFavorites(int userId, int animeId)
    {
        // Check if the user-movie relation exists
        var existingRelation = await userAnimeRepository.GetList(um => um.UserId == userId && um.AnimeId == animeId);
        if (existingRelation.Count() > 0)
        {
            // The relation exists, so remove it from the database
            var userAnimeToRemove = existingRelation.FirstOrDefault();
            await userAnimeRepository.Delete(userAnimeToRemove.Id);
        }
        else
        {
            var userAnime = existingRelation.First(); // Get the first existing entity

            userAnime.FavouriteStatus = false; // Update the FavouriteStatus

            await userAnimeRepository.Update(userAnime);
        }
    }

    public async Task AddMovieToUserViewed(int userId, int movieId)
    {
        throw new NotImplementedException();
    }
}





// Add other methods for managing the UserMovies entity as needed

