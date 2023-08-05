using Kirel.Repositories.Interfaces;

namespace Domain;

public class UserSeries : ICreatedAtTrackedEntity, IKeyEntity<int>
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public int SeriesId { get; set; }
    public bool FavouriteStatus { get; set; }
    public int Rating { get; set; }
    public int Status { get; set; }
    public User User { get; set; }
    public Series Series { get; set; }
    public DateTime Created { get; set; }
    
}