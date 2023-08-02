namespace DTOs;

public class CommentDto
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public int TitleId { get; set; }
    public string Comment { get; set; }
    public DateTime CreatedAt { get; set; }
}