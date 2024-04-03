﻿namespace Application.Features.Users.DTOs;
public sealed class UserCreateDTO
{
    public string Email { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Password { get; set; }
}
