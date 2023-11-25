#region Usings

using Moviebase.BLL.Dtos;
using Moviebase.DAL.Model;

#endregion

namespace Moviebase.BLL.Interfaces;

public interface IGenreService
{
    Task<Genre?> GetGenreAsync(string rawGenre);

    Task<Genre> CreateGenreAsync(string rawGenre);

    IAsyncEnumerable<Genre> GetGenresAsync(OMDbDto movieData);
}
