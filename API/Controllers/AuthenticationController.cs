using Application.Features.Users;
using Application.Features.Users.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[ApiController]
[Route("[controller]")]
public class AuthenticationController : ControllerBase
{
    private readonly IAuthenticationService _authenticationService;

    public AuthenticationController(IAuthenticationService authenticationService)
    {
        _authenticationService = authenticationService;
    }

    [AllowAnonymous]
    [HttpPost("Authenticate")]
    public async Task<ActionResult<AuthenticationResponse>> Authenticate(AuthenticationRequest model)
    {
        return await _authenticationService.Authenticate(model);
    }
}
