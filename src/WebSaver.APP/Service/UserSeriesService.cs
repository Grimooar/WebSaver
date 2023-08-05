using System.Linq.Expressions;
using Domain;
using Kirel.Repositories.Interfaces;

namespace WebApplication1.Service;

public class UserSeriesService
{
    
    private readonly IKirelGenericEntityFrameworkRepository<int, UserSeries> userSeriesRepository;

    public UserSeriesService(IKirelGenericEntityFrameworkRepository<int, UserSeries> userSeriesRepository)
    {
        this.userSeriesRepository = userSeriesRepository;
    }
    
     public async Task UpdateUserSeriesRating(int userId, int animeId, int newRating)
    {
        // Check if the user-anime relation already exists
        var existingRelation = await userSeriesRepository.GetList(ua => ua.UserId == userId && ua.SeriesId == animeId);
        if (existingRelation.Any())
        {
            // The relation already exists, update the rating
            var userAnime = existingRelation.FirstOrDefault();
            userAnime.Rating = newRating;
            await userSeriesRepository.Update(userAnime);
        }
        else
        {
            // The relation does not exist, so create a new one and add it to the database
            var userAnime = new UserSeries
            {
                UserId = userId,
                SeriesId = animeId,
                Rating = newRating
            };
            await userSeriesRepository.Insert(userAnime);
        }
    }

    public async Task<UserSeries> GetUserSeriesByUserIdAndSeriesId(int userId, int animeId)
    {
        // Filter by UserId and AnimeId
        Expression<Func<UserSeries, bool>> filter = ua => ua.UserId == userId && ua.SeriesId == animeId;

        // Get the first matching record
        var userSeriesList = await userSeriesRepository.GetList(filter);
        var userSeries = userSeriesList.FirstOrDefault();

        return userSeries;
    }
    public async Task AddSeriesToUserFavorites(int userId, int movieId)
    {
        // Check if the user-movie relation already exists
        var existingRelation = await userSeriesRepository.GetList(um => um.UserId == userId && um.SeriesId == movieId);
        if (existingRelation.Count() == 0)
        {
            // The relation does not exist, so create a new one and add it to the database
            var userMovie = new UserSeries
            {
                UserId = userId,
                SeriesId = movieId,
            };
            await userSeriesRepository.Insert(userMovie);
        }
        else
        {
            var userSeries = existingRelation.First(); // Get the first existing entity

            userSeries.FavouriteStatus = false; // Update the FavouriteStatus

            await userSeriesRepository.Update(userSeries);
        }
        // else: The relation already exists, do nothing (preventing duplicates)

        // You can add other logic here if needed, e.g., updating user's favorite movie count, etc.
    }

    
    public async Task UpdateUserSeriesStatus(int userId, int movieId,int newStatus)
    {
        var existingRelation = await userSeriesRepository.GetList(um => um.UserId == userId && um.SeriesId == movieId);
        if (existingRelation.Count() == 0)
        {
            // The relation does not exist, so create a new one and add it to the database
            var userMovie = new UserSeries
            {
                UserId = userId,
                Status = newStatus,
                SeriesId = movieId,
            };
            await userSeriesRepository.Insert(userMovie);
        }
        else
        {
            var userSeries = existingRelation.First(); // Get the first existing entity

            userSeries.Status = newStatus; // Update the FavouriteStatus

            await userSeriesRepository.Update(userSeries);
        }
    }
   
}