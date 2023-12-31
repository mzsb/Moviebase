﻿#region Usings

using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Moviebase.DAL.Model.Identity;
using Moviebase.DAL.Model;

#endregion

namespace Moviebase.DAL;

public class MoviebaseDbContext(DbContextOptions<MoviebaseDbContext> options) : IdentityDbContext<User, Role, Guid, IdentityUserClaim<Guid>, UserRole,
        IdentityUserLogin<Guid>, IdentityRoleClaim<Guid>, IdentityUserToken<Guid>>(options)
{
    public DbSet<Movie> Movies { get; set; }

    public DbSet<Genre> Genres { get; set; }

    public DbSet<MovieGenre> MovieGenres { get; set; }

    public DbSet<Actor> Actors { get; set; }

    public DbSet<MovieActor> MovieActors { get; set; }

    public DbSet<Review> Reviews { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.Entity<User>()
            .HasMany(user => user.UserRoles)
            .WithOne(userRole => userRole.User)
            .HasForeignKey(userRole => userRole.UserId)
            .IsRequired();

        builder.Entity<Role>()
            .HasMany(role => role.UserRoles)
            .WithOne(userRole => userRole.Role)
            .HasForeignKey(userRole => userRole.RoleId)
            .IsRequired();

        builder.Entity<User>()
            .HasMany(user => user.Reviews)
            .WithOne(review => review.User)
            .HasForeignKey(review => review.UserId)
            .IsRequired();

        builder.Entity<Movie>()
            .HasMany(movie => movie.Reviews)
            .WithOne(review => review.Movie)
            .HasForeignKey(review => review.MovieId)
            .IsRequired();

        builder.Entity<Movie>()
            .HasMany(movie => movie.MovieGenres)
            .WithOne(movieGenre => movieGenre.Movie)
            .HasForeignKey(movieGenre => movieGenre.MovieId)
            .IsRequired();

        builder.Entity<Genre>()
            .HasMany(genre => genre.MovieGenres)
            .WithOne(movieGenre => movieGenre.Genre)
            .HasForeignKey(movieGenre => movieGenre.GenreId)
            .IsRequired();

        builder.Entity<Movie>()
            .HasMany(movie => movie.MovieActors)
            .WithOne(movieActors => movieActors.Movie)
            .HasForeignKey(movieActors => movieActors.MovieId)
            .IsRequired();

        builder.Entity<Actor>()
            .HasMany(actor => actor.MovieActors)
            .WithOne(movieActors => movieActors.Actor)
            .HasForeignKey(movieActors => movieActors.ActorId)
            .IsRequired();
    }
}
