using Kirel.Repositories.Interfaces;

namespace Domain;

public class Series : ICreatedAtTrackedEntity, IKeyEntity<int>
{
    public int Id { get; set; }
    public string Name { get; set; }
    public int rating { get; set; }
    public int year { get; set; }
    public int status { get; set; }
    public string Description { get; set; }
    public string URL { get; set; }
    public DateTime Created { get; set; }
}