#region Usings

using Moviebase.BLL.Dtos;
using Moviebase.DAL.Model;

#endregion

namespace Moviebase.BLL.Extensions;

public static class OMDbExtensions
{
    public static Movie ToMovie(this OMDbDto movieData) => new()
    {
        Title = movieData.Title,
        Year = movieData.Year,
        PosterId = movieData.Poster.Split("/").Last().Split('.').First(),
        ImdbRating = decimal.TryParse(movieData.ImdbRating , out var imdbRating) ?
            imdbRating : -1
    };
}
