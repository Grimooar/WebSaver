using Microsoft.EntityFrameworkCore;
using WebApplication1.Models;

namespace WebApplication1.UserContext;

public class UserDbContext : Microsoft.EntityFrameworkCore.DbContext
{
    public UserDbContext(DbContextOptions<UserDbContext> options) : base(options)
    {
        
        
    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        
    }
    public DbSet<User> User { get; set; }
    
}