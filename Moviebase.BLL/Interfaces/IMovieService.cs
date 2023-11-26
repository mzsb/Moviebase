#region Usings

using Moviebase.BLL.Dtos;
using Moviebase.BLL.Helpers;
using Moviebase.DAL.Model;

#endregion

namespace Moviebase.BLL.Interfaces;

public interface IMovieService
{
    Task<PagedList<MovieDto>> GetPagedMoviesAsync(PaginationParams paginationParams);

    Task<MovieDto> CreateMovieByTitleAsync(CreateMovieDto createMovieDto);

    Task DeleteMovieAsync(Guid movieId);
}
