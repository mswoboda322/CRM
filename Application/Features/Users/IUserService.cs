using Application.Abstractions;
using Application.Features.Users.DTOs;

namespace Application.Features.Users;
public interface IUserService : IScopedApplicationService
{
    Task<long> CreateAsync(UserCreateDTO createDTO);
    Task<UserDTO> GetAsync(long id, bool asNoTracking = false);
    UserDTO Get(long id, bool asNoTracking = false);
    Task<List<UserDTO>> ListAsync(bool asNoTracking = false);
    List<UserDTO> List(bool asNoTracking = false);
    Task<bool> UpdateAsync(UserUpdateDTO updateDTO);
    Task DeleteAsync(long id);
}
