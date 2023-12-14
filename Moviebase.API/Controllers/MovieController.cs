#region Usings

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Moviebase.API.Extensions;
using Moviebase.BLL.Dtos;
using Moviebase.BLL.Exceptions;
using Moviebase.BLL.Helpers;
using Moviebase.BLL.Interfaces;
using System.ComponentModel;

#endregion

namespace Moviebase.API.Controllers;

[ApiController]
[Route("api/movies")]
public class MovieController(
    IMovieService movieService) : ControllerBase
{
    private const string _exampleMovieId = "35856fc5-f427-458f-a0a5-13a8ab381f33";

    private readonly IMovieService _movieService = movieService;

    [HttpGet]
    public async Task<ActionResult<IEnumerable<MovieDto>>> GetPagedMoviesAsync(
        [FromQuery] PaginationParams paginationParams)
    {
        var movies = await _movieService.GetPagedMoviesAsync(paginationParams);

        Response.AddPaginationHeader(movies.TotalCount);

        return movies;
    }

    [HttpGet("{movieId}")]
    public async Task<ActionResult<MovieDto>> GetMovieByIdAsync(
        [DefaultValue(typeof(Guid), _exampleMovieId)] Guid movieId)
    {
        try
        {
            return await _movieService.GetMovieByIdAsync(movieId);
        }
        catch(MovieException ex)
        {
            return NotFound(ex.Message);
        }
    }

    [HttpGet("title")]
    public async Task<ActionResult<IEnumerable<MovieTitleDto>>> GetMovieTitlesAsync() =>
        await _movieService.GetMovieTitlesAsync();

    //[Authorize(Policy = "RequireAdminRole")]
    [HttpPost]
    public async Task<ActionResult<MovieDto>> CreateMovieAsync(
        [FromBody] CreateMovieDto createMovieDto)
    {
        try
        {
            return await _movieService.CreateMovieByTitleAsync(createMovieDto);
        }
        catch (MovieException ex)
        {
            return BadRequest(ex.Message);
        }
    }

    //[Authorize(Policy = "RequireAdminRole")]
    [HttpDelete("{movieId}")]
    public async Task<ActionResult> DeleteMovieAsync(
        [DefaultValue(typeof(Guid), _exampleMovieId)] Guid movieId)
    {
        try
        {
            await _movieService.DeleteMovieAsync(movieId);
            return Ok("Movie deletion success");
        }
        catch (MovieException ex)
        {
            return BadRequest(ex.Message);
        }
    }
}
