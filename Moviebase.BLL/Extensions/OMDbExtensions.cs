#region Usings

using Moviebase.BLL.Dtos;
using Moviebase.DAL.Model;

#endregion

namespace Moviebase.BLL.Extensions;

public static class OMDbExtensions
{
    public static Movie ToMovie(this OMDbDto movieData) => new Movie
    {
        Title = movieData.Title,
        Year = movieData.Year,
        Genre = movieData.Genre,
        Actors = movieData.Actors,
        PosterId = movieData.Poster.Split("/").Last().Split('.').First(),
        ImdbRating = movieData.ImdbRating
    };
}
