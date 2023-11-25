#region Usings

using Moviebase.DAL.Model;

#endregion

namespace Moviebase.BLL.Interfaces;

public interface IGenreService
{
    Task<Genre?> GetGenreAsync(string rawGenre);

    Task<Genre> CreateGenreAsync(string rawGenre); 
}
