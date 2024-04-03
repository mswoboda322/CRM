using Application.Abstractions;
using Application.Features.Users.Models;

namespace Application.Features.Users;
public interface IAuthenticationService : IScopedApplicationService
{
    Task<AuthenticationResponse> Authenticate(AuthenticationRequest model);
}
