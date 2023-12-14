#region Usings

using Microsoft.EntityFrameworkCore;
using Moviebase.BLL.Dtos;
using Moviebase.BLL.Extensions;
using Moviebase.BLL.Interfaces;
using Moviebase.DAL;
using Moviebase.DAL.Model;

#endregion

namespace Moviebase.BLL.Services;

public class GenreService(MoviebaseDbContext context) : IGenreService
{
    private readonly MoviebaseDbContext _context = context;

    public async Task<Genre?> GetGenreAsync(string rawGenre) =>
        await _context.Genres.SingleOrDefaultAsync(genre => genre.Name == rawGenre);
    
    public async Task<Genre> CreateGenreAsync(string rawGenre) 
    {
        var newGenre = new Genre { Name = rawGenre };
        await _context.Genres.AddAsync(newGenre);
        await _context.SaveChangesAsync();
        return newGenre;
    }

    public async IAsyncEnumerable<Genre> GetGenresAsync(OMDbDto movieData)
    {
        foreach (var rawGenre in movieData.Genre.CommaSplit())
        {
            yield return await GetGenreAsync(rawGenre) ??
                await CreateGenreAsync(rawGenre);
        }
    }
}
