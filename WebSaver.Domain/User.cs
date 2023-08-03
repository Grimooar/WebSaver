using Kirel.Repositories.Interfaces;
using Microsoft.AspNetCore.Identity;

namespace Domain;

public class User : IdentityUser<int>, ICreatedAtTrackedEntity, IKeyEntity<int>
{
    public int Id { get; set; }
    public override string? UserName { get; set; }
    public string? Name { get; set; }
    public string? Url { get; set; }
    public string? LastName { get; set; }
    public new string? Email { get; set; }
    public DateTime Created { get; set; }

    // Добавьте свойство для связи с избранными фильмами
    public ICollection<UserMovies> FavoriteMovies { get; set; }
    public ICollection<UserAnime> FAnimes { get; set; }
    public ICollection<UserSeries> FSeries { get; set; }
}
