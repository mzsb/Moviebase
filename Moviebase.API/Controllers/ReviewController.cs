#region Usings

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Moviebase.API.Extensions;
using Moviebase.BLL.Dtos;
using Moviebase.BLL.Helpers;
using Moviebase.BLL.Interfaces;
using System.ComponentModel;

#endregion

namespace Moviebase.API.Controllers;

[ApiController]
[Route("api/reviews")]
public class ReviewController(IReviewService reviewService) : ControllerBase
{
    private const string _exampleMovieId = "35856fc5-f427-458f-a0a5-13a8ab381f33";

    [HttpGet]
    public async Task<ActionResult<IEnumerable<ReviewDto>>> GetPagedreviewsOfMovieAsync(
        [FromQuery] PaginationParams paginationParams)
    {
        var reviews = await reviewService.GetPagedReviewsAsync(paginationParams);

        Response.AddPaginationHeader(reviews.TotalCount);

        return reviews;
    }

    [HttpGet("{movieId?}")]
    public async Task<ActionResult<IEnumerable<ReviewDto>>> GetPagedReviewsOfMovieAsync(
        [DefaultValue(typeof(Guid), _exampleMovieId)] Guid movieId,
        [FromQuery] PaginationParams paginationParams)
    {
        var reviews = await reviewService.GetPagedReviewsOfMovieAsync(movieId, paginationParams);

        Response.AddPaginationHeader(reviews.TotalCount);

        return reviews;
    }

    //[Authorize(Policy = "RequireUserRole")]
    [HttpPost]
    public async Task<ActionResult<ReviewDto>> CreateReviewAsync([FromQuery] CreateReviewDto createReviewDto) =>
        await reviewService.CreateReviewAsync(createReviewDto);
}
