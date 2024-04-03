using Application.Features.Users.DTOs;
using AutoMapper;
using Domain.Entities;
using Infrastructure;
using Microsoft.AspNetCore.Identity;

namespace Application.Features.Users;
public class UserService : IUserService
{
    private readonly DatabaseContext _databaseContext;
    private readonly UserManager<User> _userManager;
    private readonly IMapper _mapper;

    public UserService(DatabaseContext databaseContext,
                       UserManager<User> userManager,
                       IMapper mapper)
    {
        _databaseContext = databaseContext;
        _userManager = userManager;
        _mapper = mapper;
    }

    public async Task<long> Create(UserCreateDTO createDTO)
    {
        var user = User.Factory.Create(createDTO.Email, createDTO.FirstName, createDTO.LastName);
        var result = await _userManager.CreateAsync(user, createDTO.Password);
        await _databaseContext.SaveChangesAsync();
        return user.Id;
    }

    public async Task<UserDTO> Get(long id)
    {
        var user = await _databaseContext.Users.FindAsync(id);
        if (user is null)
            throw new Exception("UserNotFound");
        return _mapper.Map<UserDTO>(user);
    }

    //public async Task<List<UserDTO>> List()
    //{

    //}
}
