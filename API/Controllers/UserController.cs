using Application.Features.Users;
using Application.Features.Users.DTOs;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;
[ApiController]
[Route("[controller]")]
//[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
public class UserController : ControllerBase
{
    private readonly IUserService _userService;

    public UserController(IUserService userService)
    {
        _userService = userService;
    }

    [HttpPost("Create")]
    public async Task<ActionResult<long>> Create(UserCreateDTO createDTO)
    {
        var userId = await _userService.CreateAsync(createDTO);
        return Ok(userId);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<UserDTO>> Get(long id)
    {
        return Ok(await _userService.Get(id));
    }
}
