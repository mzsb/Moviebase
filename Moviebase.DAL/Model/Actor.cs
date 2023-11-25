namespace Moviebase.DAL.Model;

public class Actor
{
    public Guid ActorId { get; set; }

    public string Name { get; set; }

    public ICollection<MovieActor> MovieActors { get; set; }
}
