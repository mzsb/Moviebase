#region Usings

using Moviebase.BLL.Dtos;
using Moviebase.DAL.Model;
using System.Globalization;

#endregion

namespace Moviebase.BLL.Extensions;

public static class OMDbExtensions
{
    private readonly static NumberFormatInfo _numberFormatInfo = new(){ NumberDecimalSeparator = "." };

    public static Movie ToMovie(this OMDbDto movieData) => new()
    {
        Title = movieData.Title,
        Year = movieData.Year,
        PosterId = movieData.Poster.Split("/").Last().Split('.').First(),
        ImdbRating = decimal.TryParse(movieData.ImdbRating, _numberFormatInfo, out var imdbRating) ?
            imdbRating : -1
    };
}
