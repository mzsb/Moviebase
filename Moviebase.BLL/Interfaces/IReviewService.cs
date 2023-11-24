#region Usings

using Moviebase.BLL.Dtos;
using Moviebase.BLL.Helpers;

#endregion

namespace Moviebase.BLL.Interfaces;

public interface IReviewService
{
    Task<PagedList<ReviewDto>> GetPagedreviewsOfMovieAsync(Guid? movieId, PaginationParams paginationParams);

    Task<ReviewDto> CreateReviewAsync(CreateReviewDto createReviewDto);
}
