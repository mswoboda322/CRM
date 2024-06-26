﻿namespace Application.Features.Users.DTOs;
public sealed class UserCreateDTO
{
    public string Email { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Password { get; set; }

    public UserCreateDTO(string email, string firstName, string lastName, string password)
    {
        Email = email;
        FirstName = firstName;
        LastName = lastName;
        Password = password;
    }
}
