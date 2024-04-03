using AutoMapper;
using Domain.Entities;

namespace Application.Features.Users.DTOs;
public class UserProfile : Profile
{
    public UserProfile()
    {
        CreateMap<User, UserDTO>()
            .PreserveReferences();
    }
}
