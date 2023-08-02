namespace Domain;

public class AnimeComments
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public int AnimeId { get; set; }
    public string Comment { get; set; }
    public DateTime CreatedAt { get; set; }
    
}