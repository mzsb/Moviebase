#region Usings

using AutoMapper;
using Moviebase.BLL.Dtos;
using Moviebase.DAL.Model;
using Moviebase.DAL.Model.Identity;

#endregion

namespace Moviebase.BLL.Helpers;

public class AutoMapperProfiles : Profile
{
    public AutoMapperProfiles()
    {
        CreateMap<Movie, MovieDto>()
            .ForMember(movieDto => movieDto.Genres, opt =>
                opt.MapFrom(movie => movie.MovieGenres.Select(movieGenre => movieGenre.Genre)))
            .ForMember(movieDto => movieDto.Actors, opt =>
                opt.MapFrom(movie => movie.MovieActors.Select(movieActor => movieActor.Actor)));
        CreateMap<Movie, MovieTitleDto>();
        CreateMap<Genre, GenreDto>();
        CreateMap<Actor, ActorDto>();
        CreateMap<User, UserDto>().ForMember(loggedUserDto => loggedUserDto.UserId, opt =>
                opt.MapFrom(user => user.Id));
        CreateMap<Review, ReviewDto>()
            .ForMember(reviewDto => reviewDto.Username, opt =>
                opt.MapFrom(review => review.User.UserName));

    }
}
