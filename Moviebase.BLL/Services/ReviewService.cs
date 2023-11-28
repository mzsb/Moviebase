#region

using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using Moviebase.BLL.Dtos;
using Moviebase.BLL.Exceptions;
using Moviebase.BLL.Extensions;
using Moviebase.BLL.Helpers;
using Moviebase.BLL.Interfaces;
using Moviebase.DAL;
using Moviebase.DAL.Model;
using System.Linq;

#endregion

namespace Moviebase.BLL.Services;

public class ReviewService(MoviebaseDbContext context, IMapper mapper) : IReviewService
{
    public async Task<PagedList<ReviewDto>> GetPagedReviewsAsync(PaginationParams paginationParams) =>
    await context.Reviews
        .OrderBy(review => review.CreationDate)
        .Include(review => review.User)
        .ProjectTo<ReviewDto>(mapper.ConfigurationProvider)
        .ToPagedListAsync(paginationParams);

    public async Task<PagedList<ReviewDto>> GetPagedReviewsOfMovieAsync(Guid movieId, PaginationParams paginationParams) =>
    await context.Reviews
        .Where(review => review.MovieId == movieId)
        .OrderBy(review => review.CreationDate)
        .Include(review => review.User)
        .ProjectTo<ReviewDto>(mapper.ConfigurationProvider)
        .ToPagedListAsync(paginationParams);

    public async Task<ReviewDto> CreateReviewAsync(CreateReviewDto createReviewDto)
    {
        if (string.IsNullOrEmpty(createReviewDto.Content)) throw new ReviewException("Invalid review content");

        var user = await context.Users.SingleOrDefaultAsync(user => user.Id == createReviewDto.UserId)
            ?? throw new ReviewException("User not exists");

        if(!await context.Movies.AnyAsync(movie => movie.MovieId == createReviewDto.MovieId))
            throw new ReviewException("Movie not exists");

        var newReview = new Review
        {
            User = user,
            MovieId = createReviewDto.MovieId,
            Content = createReviewDto.Content,
            CreationDate = DateTime.Now
        };

        await context.Reviews.AddAsync(newReview);

        var result = await context.SaveChangesAsync();

        return result > 0 ? mapper.Map<ReviewDto>(newReview) : 
            throw new ReviewException("Review creation failed");
    }

    public async Task<ReviewDto> UpdateReviewAsync(Guid reviewId, UpdateReviewDto updateReviewDto)
    {
        if (string.IsNullOrEmpty(updateReviewDto.Content)) throw new ReviewException("Invalid review content");

        var review = await context.Reviews
            .Include(review => review.User)
            .SingleOrDefaultAsync(review => review.ReviewId == reviewId)
                ?? throw new ReviewException("Review not exists");

        review.Content = updateReviewDto.Content;

        var result = await context.SaveChangesAsync();

        return result > 0 ? mapper.Map<ReviewDto>(review) :
            throw new ReviewException("Review updation failed");
    }

    public async Task DeleteReviewAsync(Guid reviewId)
    {
        if (await context.Reviews
        .Where(review => review.ReviewId == reviewId)
        .ExecuteDeleteAsync() < 1)
            throw new ReviewException("Review deletion failed");
    }
}
