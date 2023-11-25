namespace Moviebase.BLL.Dtos;

public class ReviewDto
{
    public Guid UserId { get; set; }

    public string Username { get; set; }

    public Guid MovieId { get; set; }

    public string Content { get; set; }

    public DateTime CreationDate { get; set; }
}
