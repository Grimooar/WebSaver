using Kirel.Repositories.Interfaces;

namespace WebApplication1.Models;

public class UserAnime : ICreatedAtTrackedEntity, IKeyEntity<int>
{
    public int UserId { get; set; }
    public int AnimeId { get; set; }
    public int Rating { get; set; }
    public int Status { get; set; }
    public bool FavouriteStatus { get; set; }
    public User User { get; set; }
    public Anime Anime { get; set; }
    public DateTime Created { get; set; }
    public int Id { get; set; }
}