#region Usings

using Moviebase.BLL.Helpers;

#endregion
namespace Moviebase.BLL.Extensions;

public static class PaginationExtensions
{
    public static async Task<PagedList<T>> ToPagedListAsync<T>(this IQueryable<T> source, PaginationParams paginationParams) =>
        await PagedList<T>.CreateAsync(source, paginationParams.PageNumber, paginationParams.PageSize);
}
