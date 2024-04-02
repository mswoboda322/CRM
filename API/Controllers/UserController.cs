using Application.Features.Users;
using Application.Features.Users.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;
[ApiController]
[Route("[controller]")]
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
        var userId = await _userService.Create(createDTO);
        return Ok(userId);
    }

   
}

public class LoginDTO
{
    public string Email { get; set; }
    public string Password { get; set; }
}

public class UserDTO
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
}


