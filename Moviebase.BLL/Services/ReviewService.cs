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

#endregion

namespace Moviebase.BLL.Services;

public class ReviewService(MoviebaseDbContext context, IMapper mapper) : IReviewService
{
    public async Task<ReviewDto> CreateReviewAsync(CreateReviewDto createReviewDto)
    {
        var user = await context.Users.SingleOrDefaultAsync(user => user.UserName == createReviewDto.Username)
            ?? throw new ReviewException("Invalid user");

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

        return result > 0 ? new ReviewDto
        {
            Username = user.UserName,
            Content = newReview.Content,
            CreationDate = newReview.CreationDate
        } : throw new ReviewException("Review creation failed");
    }

    public async Task<PagedList<ReviewDto>> GetPagedreviewsOfMovieAsync(Guid? movieId, PaginationParams paginationParams) =>
        await context.Reviews
            .Where(review => movieId == null ? true : review.MovieId == movieId)
            .OrderBy(review => review.CreationDate)
            .Include(review => review.User)
            .ProjectTo<ReviewDto>(mapper.ConfigurationProvider)
            .ToPagedListAsync(paginationParams);
}
