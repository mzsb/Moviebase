#region Usings

using AutoMapper;
using Moviebase.BLL.Dtos;
using Moviebase.DAL.Model;

#endregion

namespace Moviebase.BLL.Helpers;

public class AutoMapperProfiles : Profile
{
    public AutoMapperProfiles()
    {
        CreateMap<Movie, MovieDto>();
        CreateMap<Review, ReviewDto>().ForMember(reviewDto => reviewDto.Username, opt =>
            opt.MapFrom(review => review.User.UserName));
    }
}
