#region Usings

using Moviebase.BLL.Dtos;
using Moviebase.DAL.Model;

#endregion

namespace Moviebase.BLL.Interfaces;

public interface IActorService
{
    Task<Actor?> GetActorAsync(string rawActor);

    Task<Actor> CreateActorAsync(string rawActor);

    IAsyncEnumerable<Actor> GetActorsAsync(OMDbDto movieData);
}
