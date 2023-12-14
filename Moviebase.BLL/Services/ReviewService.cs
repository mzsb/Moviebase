#region Usings

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

public class ReviewService(
    MoviebaseDbContext context, 
    IMapper mapper) : IReviewService
{
    private readonly MoviebaseDbContext _context = context;
    private readonly IMapper _mapper = mapper;

    public async Task<PagedList<ReviewDto>> GetPagedReviewsAsync(PaginationParams paginationParams) =>
    await _context.Reviews
        .OrderBy(review => review.CreationDate)
        .Include(review => review.User)
        .ProjectTo<ReviewDto>(_mapper.ConfigurationProvider)
        .ToPagedListAsync(paginationParams);

    public async Task<PagedList<ReviewDto>> GetPagedReviewsOfMovieAsync(Guid movieId, PaginationParams paginationParams)
    {
        if (!await _context.Movies.AnyAsync(movie => movie.MovieId == movieId))
            throw new ReviewException("Movie not exists");

        return await _context.Reviews
            .Where(review => review.MovieId == movieId)
            .OrderBy(review => review.CreationDate)
            .Include(review => review.User)
            .ProjectTo<ReviewDto>(_mapper.ConfigurationProvider)
            .ToPagedListAsync(paginationParams);
    }

    public async Task<ReviewDto> CreateReviewAsync(CreateReviewDto createReviewDto)
    {
        if (string.IsNullOrEmpty(createReviewDto.Content)) throw new ReviewException("Invalid review content");

        var user = await _context.Users.SingleOrDefaultAsync(user => user.Id == createReviewDto.UserId)
            ?? throw new ReviewException("User not exists");

        if(!await _context.Movies.AnyAsync(movie => movie.MovieId == createReviewDto.MovieId))
            throw new ReviewException("Movie not exists");

        var newReview = new Review
        {
            User = user,
            MovieId = createReviewDto.MovieId,
            Content = createReviewDto.Content,
            CreationDate = DateTime.Now,
            LastUpdationDate = DateTime.Now
        };

        await _context.Reviews.AddAsync(newReview);

        var result = await _context.SaveChangesAsync();

        return result > 0 ? _mapper.Map<ReviewDto>(newReview) : 
            throw new ReviewException("Review creation failed");
    }

    public async Task<ReviewDto> UpdateReviewAsync(Guid reviewId, UpdateReviewDto updateReviewDto)
    {
        if (string.IsNullOrEmpty(updateReviewDto.Content)) throw new ReviewException("Invalid review content");

        var review = await _context.Reviews
            .Include(review => review.User)
            .SingleOrDefaultAsync(review => review.ReviewId == reviewId)
                ?? throw new ReviewException("Review not exists");

        review.Content = updateReviewDto.Content;
        review.LastUpdationDate = DateTime.Now;

        var result = await _context.SaveChangesAsync();

        return result > 0 ? _mapper.Map<ReviewDto>(review) :
            throw new ReviewException("Review updation failed");
    }

    public async Task DeleteReviewAsync(Guid reviewId)
    {
        if (await _context.Reviews
        .Where(review => review.ReviewId == reviewId)
        .ExecuteDeleteAsync() < 1)
            throw new ReviewException("Review deletion failed");
    }
}
