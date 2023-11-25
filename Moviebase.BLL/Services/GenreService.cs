#region Usings

using Microsoft.EntityFrameworkCore;
using Moviebase.BLL.Interfaces;
using Moviebase.DAL;
using Moviebase.DAL.Model;

#endregion

namespace Moviebase.BLL.Services;

public class GenreService(MoviebaseDbContext context) : IGenreService
{
    public async Task<Genre?> GetGenreAsync(string rawGenre) =>
        await context.Genres.SingleOrDefaultAsync(genre => genre.Name == rawGenre);
    
    public async Task<Genre> CreateGenreAsync(string rawGenre) 
    {
        var newGenre = new Genre { Name = rawGenre };
        await context.Genres.AddAsync(newGenre);
        await context.SaveChangesAsync();
        return newGenre;
    }
}
