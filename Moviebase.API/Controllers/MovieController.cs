#region Usings

using Microsoft.AspNetCore.Mvc;
using Moviebase.API.Extensions;
using Moviebase.BLL.Dtos;
using Moviebase.BLL.Helpers;
using Moviebase.BLL.Interfaces;

#endregion

namespace Moviebase.API.Controllers;

[ApiController]
[Route("api/movies")]
public class MovieController(IMovieService movieService) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<IEnumerable<MovieDto>>> GetPagedMoviesAsync([FromQuery] PaginationParams paginationParams)
    {
        var movies = await movieService.GetPagedMoviesAsync(paginationParams);

        Response.AddPaginationHeader(movies.TotalCount);

        return movies;
    }

    [HttpPost]
    public async Task<ActionResult<MovieDto>> CreateMovieAsync([FromBody] CreateMovieDto createMovieDto) =>
        await movieService.CreateMovieByTitleAsync(createMovieDto);
}
