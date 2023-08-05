using Kirel.Repositories.Interfaces;

namespace Domain;

public class Movie : ICreatedAtTrackedEntity, IKeyEntity<int>
{
    public int Id { get; set; }
    public string Title { get; set; }
    public int Year { get; set; }
    public int Rating { get; set; }
    public string Poster { get; set; }
    public string Description { get; set; }
    public int Status { get; set; } // Статус фильма (например, просмотрено, избранное)

    // Добавьте поле для связи с пользователями
    public ICollection<UserMovies> Users { get; set; }
    public DateTime Created { get; set; }

    
}
