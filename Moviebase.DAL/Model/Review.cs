using Moviebase.DAL.Model.Identity;

namespace Moviebase.DAL.Model;

public class Review
{
    public Guid ReviewId { get; set; }

    public Guid UserId { get; set; }
    public User User { get; set; }

    public Guid MovieId { get; set; }
    public Movie Movie { get; set; }

    public string Content { get; set; }

    public DateTime CreationDate { get; set; }

    public DateTime LastUpdationDate { get; set; }
}
