﻿#region Usings

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
    IOMDbService omdbService,
    IGenreService genreService,
    IActorService actorService) : IMovieService
{
    public async Task<PagedList<MovieDto>> GetPagedMoviesAsync(PaginationParams paginationParams) =>
        await context.Movies
            .OrderBy(movie => movie.Title)
            .Include(movie => movie.MovieGenres)
            .ThenInclude(movieGenre => movieGenre.Genre)
            .Include(movie => movie.MovieActors)
            .ThenInclude(movieGenre => movieGenre.Actor)
            .ProjectTo<MovieDto>(mapper.ConfigurationProvider)
            .ToPagedListAsync(paginationParams);

    public async Task<MovieDto> CreateMovieByTitleAsync(CreateMovieDto createMovieDto)
    {
        var newMovie = await context.Movies.SingleOrDefaultAsync(movie => movie.Title.ToLower() == createMovieDto.Title.ToLower());

        if (newMovie is null)
        {
            var movieData = await omdbService.GetMovieDataByTitleAsync(createMovieDto.Title);

            newMovie = movieData.ToMovie();

            await context.Movies.AddAsync(newMovie);

            await foreach (var genre in genreService.GetGenresAsync(movieData))
            {
                await context.MovieGenres.AddAsync(new() { Movie = newMovie, Genre = genre });
            }

            await foreach (var actor in actorService.GetActorsAsync(movieData))
            {
                await context.MovieActors.AddAsync(new() { Movie = newMovie, Actor = actor });
            }

            if (await context.SaveChangesAsync() < 1) throw new MovieException("Movie creation failed");
        }

        return mapper.Map<MovieDto>(newMovie);
    }
}
