namespace Moviebase.BLL.Dtos;

public class MovieDto
{
    public Guid MovieId { get; set; }

    public string Title { get; set; }

    public string Year { get; set; }

    public string PosterId { get; set; }

    public decimal ImdbRating { get; set; }

    public IEnumerable<GenreDto> Genres { get; set; }

    public IEnumerable<ActorDto> Actors { get; set; }
}
