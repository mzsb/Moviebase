#region Usings

using Moviebase.BLL.Dtos;
using Moviebase.DAL.Model;
using System.Globalization;
using System.Text.RegularExpressions;

#endregion

namespace Moviebase.BLL.Extensions;

public static partial class OMDbExtensions
{
    private static readonly Regex _posterUrlRegex = PosterUrlRegex();

    [GeneratedRegex("https://m.media-amazon.com/images/M/.+._V1_SX300.jpg", RegexOptions.Compiled)]
    private static partial Regex PosterUrlRegex();

    private static readonly NumberFormatInfo _numberFormatInfo = new(){ NumberDecimalSeparator = "." };

    public static Movie ToMovie(this OMDbDto movieData) => new()
    {
        Title = movieData.Title,
        Year = movieData.Year,
        PosterId = movieData.Poster.ToPosterId(),
        ImdbRating = movieData.ImdbRating.ToDecimal()
    };

    public static string ToPosterId(this string posterUrl) =>
        _posterUrlRegex.IsMatch(posterUrl) ? 
            posterUrl[36..^14] : 
            string.Empty;

    public static decimal ToDecimal(this string str) =>
        decimal.TryParse(str, _numberFormatInfo, out var @decimal) ?
            @decimal :
            -1;
}
