#region Usings

using Microsoft.AspNetCore.Mvc;
using Moviebase.API.Extensions;
using Moviebase.BLL.Dtos;
using Moviebase.BLL.Helpers;
using Moviebase.BLL.Interfaces;
using System.ComponentModel;

#endregion

namespace Moviebase.API.Controllers;

[ApiController]
[Route("api/movies")]
public class MovieController(
    IMovieService movieService,
    IConfiguration configurationű) : ControllerBase
{
    private const string _exampleMovieId = "35856fc5-f427-458f-a0a5-13a8ab381f33";

    [HttpGet]
    public async Task<ActionResult<IEnumerable<MovieDto>>> GetPagedMoviesAsync([FromQuery] PaginationParams paginationParams)
    {
        var movies = await movieService.GetPagedMoviesAsync(paginationParams);

        Response.AddPaginationHeader(movies.TotalCount);

        return movies;
    }

    //[Authorize(Policy = "RequireAdminRole")]
    [HttpPost]
    public async Task<ActionResult<MovieDto>> CreateMovieAsync([FromBody] CreateMovieDto createMovieDto) =>
        await movieService.CreateMovieByTitleAsync(createMovieDto);

    [HttpDelete("{movieId}")]
    public async Task DeleteMovieAsync([DefaultValue(typeof(Guid), _exampleMovieId)]  Guid movieId) => 
        await movieService.DeleteMovieAsync(movieId);
}
