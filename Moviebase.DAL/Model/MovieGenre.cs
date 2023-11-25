namespace Moviebase.DAL.Model;

public class MovieGenre
{
    public Guid MovieGenreId { get; set; }

    public Guid MovieId { get; set; }

    public Movie Movie { get; set; }

    public Guid GenreId { get; set; }

    public Genre Genre { get; set; }
}
