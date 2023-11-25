namespace Moviebase.DAL.Model;

public class Genre
{
    public Guid GenreId { get; set; }

    public string Name { get; set; }

    public ICollection<MovieGenre> MovieGenres { get; set; }
}
