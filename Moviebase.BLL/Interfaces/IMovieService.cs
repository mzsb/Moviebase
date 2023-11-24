#region Usings

using Moviebase.BLL.Dtos;
using Moviebase.BLL.Helpers;

#endregion

namespace Moviebase.BLL.Interfaces;

public interface IMovieService
{
    Task<PagedList<MovieDto>> GetPagedMoviesAsync(PaginationParams paginationParams);

    Task<MovieDto> CreateMovieByTitleAsync(CreateMovieDto createMovieDto);
}
