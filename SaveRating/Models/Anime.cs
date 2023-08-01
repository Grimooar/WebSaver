using Kirel.Repositories.Interfaces;

namespace WebApplication1.Models;

public class Anime : ICreatedAtTrackedEntity, IKeyEntity<int>
{
    public int Id { get; set; }
    public string Name { get; set; }
    public int Rating { get; set; }
    
    
    public string Description { get; set; }
    public string URL { get; set; }
    public DateTime Created { get; set; }
}