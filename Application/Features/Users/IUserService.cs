using Application.Abstractions;
using Application.Features.Users.DTOs;

namespace Application.Features.Users;
public interface IUserService : IScopedApplicationService
{
    Task<long> Create(UserCreateDTO createDTO);
    Task<UserDTO> Get(long id);
}
