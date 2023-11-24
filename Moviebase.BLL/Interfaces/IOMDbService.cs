#region Usings

using Moviebase.BLL.Dtos;

#endregion

namespace Moviebase.BLL.Interfaces;

public interface IOMDbService
{
    Task<OMDbDto> GetMovieDataByTitleAsync(string title);

    IEnumerable<OMDbDto> GetMovieDatasFromJSONFile(string filePath);
}
