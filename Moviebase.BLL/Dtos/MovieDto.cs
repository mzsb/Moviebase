namespace Moviebase.BLL.Dtos;

public class MovieDto
{
    public Guid MovieId { get; set; }

    public string Title { get; set; }

    public string Year { get; set; }

    public string Genre { get; set; }

    public string Actors { get; set; }

    public string PosterId { get; set; }

    public decimal ImdbRating { get; set; }
}
