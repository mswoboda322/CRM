using Application.Features.Users.DTOs;
using Domain.Entities;
using Infrastructure;
using Microsoft.AspNetCore.Identity;

namespace Application.Features.Users;
public class UserService : IUserService
{
    private readonly DatabaseContext _databaseContext;
    private readonly UserManager<User> _userManager;

    public UserService(DatabaseContext databaseContext, UserManager<User> userManager)
    {
        _databaseContext = databaseContext;
        _userManager = userManager;
    }

    public async Task<long> Create(UserCreateDTO createDTO)
    {
        var user = User.Factory.Create(createDTO.Email, createDTO.FirstName, createDTO.LastName);
        var result = await _userManager.CreateAsync(user, createDTO.Password);
        await _databaseContext.SaveChangesAsync();
        return user.Id;
    }
}
