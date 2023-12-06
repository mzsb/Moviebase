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
    private readonly Guid _adminId = Guid.Parse("a25cbf32-d93d-4ab9-984f-21290f393d8c");
    private readonly Guid _exampleUserId = Guid.Parse("e214ce42-f4f4-4859-ba1d-db6ab1f21f75");
    private readonly Guid _exampleMovieId = Guid.Parse("35856fc5-f427-458f-a0a5-13a8ab381f33");
    private readonly Guid _exampleReviewId = Guid.Parse("63d40f42-506a-4794-93f8-8378dfe5d600");
    private readonly string _adminName = "admin";
    private readonly string _adminPassword = "admin";
    private readonly string _adminRole = "Admin";
    private readonly string _userRole = "User";
    private readonly int _userCount = 1;
    private readonly int _reviwsPerUserForMovieCount = 1;

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

        var adminUser = new User 
        { 
            Id = _adminId, 
            UserName = _adminName,
            Email = "admin@examp.le"
        };

        await userManager.CreateAsync(adminUser, _adminPassword);
        await userManager.AddToRoleAsync(adminUser, _adminRole);
        await userManager.AddToRoleAsync(adminUser, _userRole);

        var userFaker = new Faker<User>()
            .RuleFor(user => user.UserName, x => x.Name.FirstName())
            .RuleFor(user => user.Created, x => x.Date.Past(2));

        var isFirst = true;
        foreach (var user in userFaker.GenerateLazy(_userCount))
        {
            if (isFirst) { user.Id = _exampleUserId; isFirst = false; }

            user.Email = $"{user.UserName?.ToLower() ?? "email"}@examp.le";

            await userManager.CreateAsync(user, user.UserName ?? "password");
            await userManager.AddToRoleAsync(user, _userRole);
        }
    }

    private async Task SeedMoviesAsync()
    {
        var isFirst = true;
        foreach (var movieData in omdbService.GetMovieDatasFromJSONFile("../Moviebase.DAL/movieSeed.json"))
        {
            var newMovie = movieData.ToMovie();

            if (isFirst) { newMovie.MovieId = _exampleMovieId; isFirst = false; }

            await context.AddAsync(newMovie);

            await foreach (var genre in genreService.GetGenresAsync(movieData))
            {
                await context.MovieGenres.AddAsync(new() { Movie = newMovie, Genre = genre });
            }

            await foreach (var actor in actorService.GetActorsAsync(movieData))
            {
                await context.MovieActors.AddAsync(new() { Movie = newMovie, Actor = actor });
            }
        }
        await context.SaveChangesAsync();
    }

    private async Task SeedReviewsAsync()
    {
        var reviewFaker = new Faker<Review>()
            .RuleFor(user => user.Content, x => x.Lorem.Text())
            .RuleFor(user => user.CreationDate, x => x.Date.Between(DateTime.Now.AddDays(-14), DateTime.Now.AddDays(-7)))
            .RuleFor(user => user.LastUpdationDate, x => x.Date.Between(DateTime.Now.AddDays(-6), DateTime.Now.AddDays(-1)));

        var isFirst = true;
        foreach (var movie in await context.Movies.ToListAsync())
        {
            foreach (var user in await userManager.Users.ToListAsync())
            {
                foreach (var review in reviewFaker.GenerateLazy(_reviwsPerUserForMovieCount))
                {
                    if (isFirst) { review.ReviewId = _exampleReviewId; isFirst = false; }
                    
                    review.User = user;
                    review.Movie = movie;

                    await context.AddAsync(review);
                }
            }
        }

        await context.SaveChangesAsync();
    }
}
