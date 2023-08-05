namespace DTOs;

public class CommentUpdateDto
{
    public int UserId { get; set; }
    public int TitleId { get; set; }
    public string Comment { get; set; }
}