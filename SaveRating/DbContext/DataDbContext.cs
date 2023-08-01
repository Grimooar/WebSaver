using Microsoft.EntityFrameworkCore;
using WebApplication1.Models;
using WebApplication1.Service;

namespace WebApplication1.DbContext;

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
    
}