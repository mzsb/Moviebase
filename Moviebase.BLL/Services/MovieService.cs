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

public class MovieService(
    MoviebaseDbContext context, 
    IMapper mapper,
    IOMDbService omdbService) : IMovieService
{
    public async Task<PagedList<MovieDto>> GetPagedMoviesAsync(PaginationParams paginationParams) =>
        await context.Movies
            .OrderBy(movie => movie.Title)
            .ProjectTo<MovieDto>(mapper.ConfigurationProvider)
            .ToPagedListAsync(paginationParams);

    public async Task<MovieDto> CreateMovieByTitleAsync(CreateMovieDto createMovieDto)
    {
      
        var exsistingMovie = await GetMovieByTitleAsync(createMovieDto.Title);

        if(exsistingMovie is null)
        {
            var movieData = await omdbService.GetMovieDataByTitleAsync(createMovieDto.Title);

            var newMovie = movieData.ToMovie();

            await context.Movies.AddAsync(newMovie);

            if(await context.SaveChangesAsync() < 1) throw new MovieException("Movie creation failed");

            exsistingMovie = await GetMovieByTitleAsync(newMovie.Title);
        }

        return mapper.Map<MovieDto>(exsistingMovie);
    }

    private async Task<Movie?> GetMovieByTitleAsync(string title) =>
        await context.Movies.SingleOrDefaultAsync(movie => movie.Title.ToLower() == title.ToLower());
}
