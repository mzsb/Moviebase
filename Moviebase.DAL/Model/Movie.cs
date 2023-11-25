namespace Moviebase.DAL.Model;

public class Movie
{
    public Guid MovieId { get; set; }

    public string Title { get; set; }

    public string Year { get; set; }

    public string PosterId { get; set; }

    public decimal ImdbRating { get; set; }

    public ICollection<MovieGenre> MovieGenres { get; set; }

    public ICollection<MovieActor> MovieActors { get; set; }

    public ICollection<Review>? Reviews { get; set; }
}
