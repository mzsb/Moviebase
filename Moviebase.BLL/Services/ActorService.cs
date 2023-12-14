#region Usings

using Microsoft.EntityFrameworkCore;
using Moviebase.BLL.Dtos;
using Moviebase.BLL.Extensions;
using Moviebase.BLL.Interfaces;
using Moviebase.DAL;
using Moviebase.DAL.Model;

#endregion

namespace Moviebase.BLL.Services;

public class ActorService(MoviebaseDbContext context) : IActorService
{
    private readonly MoviebaseDbContext _context = context;

    public async Task<Actor?> GetActorAsync(string rawActor) =>
        await _context.Actors.SingleOrDefaultAsync(actor => actor.Name == rawActor);

    public async Task<Actor> CreateActorAsync(string rawActor)
    {
        var newActor = new Actor { Name = rawActor };
        await _context.Actors.AddAsync(newActor);
        await _context.SaveChangesAsync();
        return newActor;
    }

    public async IAsyncEnumerable<Actor> GetActorsAsync(OMDbDto movieData)
    {
        foreach (var rawActor in movieData.Actors.CommaSplit())
        {
            yield return await GetActorAsync(rawActor) ??
                await CreateActorAsync(rawActor);
        }
    }
}
