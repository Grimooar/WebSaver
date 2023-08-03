using Kirel.Repositories.Interfaces;

namespace Domain;


public class UserMovies : ICreatedAtTrackedEntity, IKeyEntity<int>
{
    
    public int UserId { get; set; }
    public int MovieId { get; set; }
    public int Rating { get; set; }
    public bool FavouriteStatus { get; set; }
    public int Status { get; set; }
    public User User { get; set; }
    public Movie Movie { get; set; }
    public DateTime Created { get; set; }
    public int Id { get; set; }
}
