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
        PosterId = movieData.Poster.ToPosterId(),
        ImdbRating = movieData.ImdbRating.ToDecimal()
    };

    public static string ToPosterId(this string posterUrl) =>
        posterUrl != "N/A" ? 
            posterUrl.Split("/").Last().Split('.').First() : 
            string.Empty;

    public static decimal ToDecimal(this string str) =>
        decimal.TryParse(str, _numberFormatInfo, out var @decimal) ?
            @decimal :
            -1;
}
