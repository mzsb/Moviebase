#region Usings

using Microsoft.Extensions.Configuration;
using Moviebase.BLL.Dtos;
using Moviebase.BLL.Exceptions;
using Moviebase.BLL.Extensions;
using Moviebase.BLL.Interfaces;
using Newtonsoft.Json;


#endregion

namespace Moviebase.BLL.Services;

public class OMDbService(
    IConfiguration configuration, 
    HttpClient httpClient) : IOMDbService
{
    private readonly string _baseUrl = "https://www.omdbapi.com";
    private readonly string _apiKey = configuration.GetValue("OMDbAPIKey");

    public async Task<OMDbDto> GetMovieDataByTitleAsync(string title)
    {
        var resopnse = await httpClient.GetAsync($"{_baseUrl}/?t={title}&apikey={_apiKey}");

        if (!resopnse.IsSuccessStatusCode) throw new OMDbException("Getting movie data failed");

        var movieDataJson = await resopnse.Content.ReadAsStringAsync();

        return JsonConvert.DeserializeObject<OMDbDto>(movieDataJson) ?? throw new OMDbException("Parsing movie data failed");
    }

    public IEnumerable<OMDbDto> GetMovieDatasFromJSONFile(string filePath)
    {
        using StreamReader streamReader = new(filePath);
        string json = streamReader.ReadToEnd();
        return JsonConvert.DeserializeObject<List<OMDbDto>>(json) ?? throw new OMDbException("Parsing movie data from file failed");
    }
}
