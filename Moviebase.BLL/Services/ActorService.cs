#region Usings

using Microsoft.EntityFrameworkCore;
using Moviebase.BLL.Interfaces;
using Moviebase.DAL;
using Moviebase.DAL.Model;

#endregion

namespace Moviebase.BLL.Services;

public class ActorService(MoviebaseDbContext context) : IActorService
{
    public async Task<Actor?> GetActorAsync(string rawActor) =>
        await context.Actors.SingleOrDefaultAsync(actor => actor.Name == rawActor);

    public async Task<Actor> CreateActorAsync(string rawActor)
    {
        var newActor = new Actor { Name = rawActor };
        await context.Actors.AddAsync(newActor);
        await context.SaveChangesAsync();
        return newActor;
    }
}
