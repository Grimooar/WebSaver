using Domain;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.DbContext;

public class DataDbContext :  Microsoft.EntityFrameworkCore.DbContext
{
    public DataDbContext(DbContextOptions<DataDbContext> options) : base(options)
    {

    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        
    }

    private DbSet<Anime> Anime { get; set; }
    private DbSet<Movie> Movies { get; set; }
    private DbSet<UserMovies> UserMovies { get; set; }
    private DbSet<Series> Series { get; set; }
    private DbSet<UserAnime> UserAnimes { get; set; }
    private DbSet<UserSeries> UserSeries { get; set; }
    private DbSet<AnimeComments> AnimeComments { get; set; }
    private DbSet<MovieComment> MovieComments { get; set; }
    private DbSet<SeriesComment> SeriesComments { get; set; }

}