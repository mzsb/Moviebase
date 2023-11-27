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
[Route("api/reviews")]
public class ReviewController(IReviewService reviewService) : ControllerBase
{
    private const string _exampleReviewId = "63d40f42-506a-4794-93f8-8378dfe5d600";
    private const string _exampleMovieId = "35856fc5-f427-458f-a0a5-13a8ab381f33";

    [HttpGet]
    public async Task<ActionResult<IEnumerable<ReviewDto>>> GetPagedreviewsOfMovieAsync(
        [FromQuery] PaginationParams paginationParams)
    {
        var reviews = await reviewService.GetPagedReviewsAsync(paginationParams);

        Response.AddPaginationHeader(reviews.TotalCount);

        return reviews;
    }

    [HttpGet("{movieId}")]
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
    public async Task<ActionResult<ReviewDto>> CreateReviewAsync([FromQuery] CreateReviewDto createReviewDto)
    {
        try
        {
            return await reviewService.CreateReviewAsync(createReviewDto);
        } catch(ReviewException ex)
        {
            return BadRequest(ex.Message);
        }
    }

    //[Authorize(Policy = "RequireUserRole")]
    [HttpPut("{reviewId}")]
    public async Task<ActionResult<ReviewDto>> UpdateReviewAsync(
        [DefaultValue(typeof(Guid), _exampleReviewId)] Guid reviewId,
        UpdateReviewDto updateReviewDto)
    {
        try
        {
            return await reviewService.UpdateReviewAsync(reviewId, updateReviewDto);
        }
        catch (ReviewException ex)
        {
            return BadRequest(ex.Message);
        }
    }

    //[Authorize(Policy = "RequireUserRole")]
    [HttpDelete("{reviewId}")]
    public async Task<ActionResult> DeleteReviewAsync(
        [DefaultValue(typeof(Guid), _exampleReviewId)] Guid reviewId)
    {
        try
        {
            await reviewService.DeleteReviewAsync(reviewId);
            return Ok("Review deletion success");
        }
        catch (ReviewException ex)
        {
            return BadRequest(ex.Message);
        }
    }
}
