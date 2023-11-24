namespace Moviebase.BLL.Dtos;

public class CreateReviewDto
{
    public string Username { get; set; }

    public Guid MovieId { get; set; }

    public string Content { get; set; }
}
