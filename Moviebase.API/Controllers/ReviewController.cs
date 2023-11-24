#region Usings

using Microsoft.AspNetCore.Mvc;
using Moviebase.API.Extensions;
using Moviebase.BLL.Dtos;
using Moviebase.BLL.Helpers;
using Moviebase.BLL.Interfaces;

#endregion

namespace Moviebase.API.Controllers;

[ApiController]
[Route("api/reviews")]
public class ReviewController(IReviewService reviewService) : ControllerBase
{
    [HttpGet("/")]
    [HttpGet("/{movieId?}")]
    public async Task<ActionResult<IEnumerable<ReviewDto>>> GetPagedreviewsOfMovieAsync(
        Guid? movieId,
        [FromQuery] PaginationParams paginationParams)
    {
        var reviews = await reviewService.GetPagedreviewsOfMovieAsync(movieId, paginationParams);

        Response.AddPaginationHeader(reviews.TotalCount);

        return reviews;
    }

    [HttpPost]
    public async Task<ActionResult<ReviewDto>> CreateReviewAsync([FromQuery] CreateReviewDto createReviewDto) =>
        await reviewService.CreateReviewAsync(createReviewDto);
}
