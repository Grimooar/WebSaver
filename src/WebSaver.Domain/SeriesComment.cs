namespace Domain;

public class SeriesComment
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public int SeriesId { get; set; }
    public string Comment { get; set; }
    public DateTime CreatedAt { get; set; }
}