#region Usings

using Bogus;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Moviebase.BLL.Extensions;
using Moviebase.BLL.Interfaces;
using Moviebase.DAL;
using Moviebase.DAL.Model;
using Moviebase.DAL.Model.Identity;

#endregion

namespace Moviebase.BLL.Services;

public class SeedService(
        UserManager<User> userManager,
        RoleManager<Role> roleManager,
        IOMDbService omdbService,
        IGenreService genreService,
        IActorService actorService,
        MoviebaseDbContext context) : ISeedService
{
    private readonly string _adminName = "admin";
    private readonly string _adminPassword = "admin";
    private readonly string _adminRole = "Admin";
    private readonly string _userRole = "User";
    private readonly int _userCount = 1;
    private readonly int _reviwPerUserForMovieCount = 1;

    public async Task Seed()
    {
        await SeedUsersAsync();
        await SeedMoviesAsync();
        await SeedReviewsAsync();
    }

    private async Task SeedUsersAsync()
    {
        await roleManager.CreateAsync(new Role { Name = _adminRole });
        await roleManager.CreateAsync(new Role { Name = _userRole });

        var adminUser = new User { UserName = _adminName };

        await userManager.CreateAsync(adminUser, _adminPassword);
        await userManager.AddToRoleAsync(adminUser, _adminRole);
        await userManager.AddToRoleAsync(adminUser, _userRole);

        var userFaker = new Faker<User>()
            .RuleFor(user => user.UserName, x => x.Name.FirstName())
            .RuleFor(user => user.Created, x => x.Date.Past(2));

        foreach (var user in userFaker.GenerateLazy(_userCount))
        {
            await userManager.CreateAsync(user, user.UserName ?? "Password");
            await userManager.AddToRoleAsync(user, _userRole);
        }
    }

    private async Task SeedMoviesAsync()
    {
        foreach (var movieData in omdbService.GetMovieDatasFromJSONFile("../Moviebase.DAL/movieSeed.json"))
        {
            var newMovie = movieData.ToMovie();

            newMovie.MovieGenres = new List<MovieGenre>();
            foreach (var rawGenre in movieData.Genre.CommaSplit())
            {
                var genre = await genreService.GetGenreAsync(rawGenre) ??
                    await genreService.CreateGenreAsync(rawGenre);

                newMovie.MovieGenres.Add(new() { Movie = newMovie, Genre = genre });
            }

            newMovie.MovieActors = new List<MovieActor>();
            foreach (var rawActor in movieData.Actors.CommaSplit())
            {
                var actor = await actorService.GetActorAsync(rawActor) ??
                    await actorService.CreateActorAsync(rawActor);

                newMovie.MovieActors.Add(new() { Movie = newMovie, Actor = actor });
            }

            await context.AddAsync(newMovie);
        }
        await context.SaveChangesAsync();
    }

    private async Task SeedReviewsAsync()
    {
        var movies = await context.Movies.ToListAsync();

        var users = await userManager.Users.ToListAsync();

        var reviewFaker = new Faker<Review>()
            .RuleFor(user => user.Content, x => x.Lorem.Text())
            .RuleFor(user => user.CreationDate, x => x.Date.Past(2));

        foreach (var movie in movies)
        {
            foreach (var user in users)
            {
                foreach (var review in reviewFaker.GenerateLazy(_reviwPerUserForMovieCount))
                {
                    review.User = user;
                    review.Movie = movie;

                    await context.AddAsync(review);
                }
            }
        }

        await context.SaveChangesAsync();
    }
}
