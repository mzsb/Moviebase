namespace Moviebase.DAL.Model;

public class MovieActor
{
    public Guid MovieActorId { get; set; }

    public Guid MovieId { get; set; }

    public Movie Movie { get; set; }

    public Guid ActorId { get; set; }

    public Actor Actor { get; set; }
}
