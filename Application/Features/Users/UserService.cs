using Application.Features.Users.DTOs;
using AutoMapper;
using Domain.Entities;
using Infrastructure;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

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

    public async Task<long> CreateAsync(UserCreateDTO createDTO)
    {
        var user = User.Factory.Create(createDTO.Email, createDTO.FirstName, createDTO.LastName);
        var result = await _userManager.CreateAsync(user, createDTO.Password);
        await _databaseContext.SaveChangesAsync();
        return user.Id;
    }

    public async Task<UserDTO> GetAsync(long id, bool asNoTracking = false)
    {
        var user = await _databaseContext.Users.FirstOrDefaultAsync(x => x.Id == id);
        if (user is null)
            throw new Exception("UserNotFound");
        return _mapper.Map<UserDTO>(user);
    }

    public UserDTO Get(long id, bool asNoTracking = false)
    {
        var user = new User();
        if (asNoTracking)
            user = _databaseContext.Users.AsNoTracking().FirstOrDefault(x => x.Id == id);
        else
            user = _databaseContext.Users.FirstOrDefault(x => x.Id == id);
        if (user is null)
            throw new Exception("UserNotFound");
        return _mapper.Map<UserDTO>(user);
    }

    public async Task<List<UserDTO>> ListAsync(bool asNoTracking = false)
    {
        var users = new List<User>();
        if (asNoTracking)
            users = await _databaseContext.Users.AsNoTracking().ToListAsync();
        else
            users = await _databaseContext.Users.ToListAsync();

        return _mapper.Map<List<UserDTO>>(users);
    }

    public List<UserDTO> List(bool asNoTracking = false)
    {
        var users = new List<User>();
        if (asNoTracking)
            users = _databaseContext.Users.AsNoTracking().ToList();
        else
            users = _databaseContext.Users.ToList();

        return _mapper.Map<List<UserDTO>>(users);
    }

    public async Task<bool> UpdateAsync(UserUpdateDTO updateDTO)
    {
        var user = await _databaseContext.Users.FirstOrDefaultAsync(x => x.Id == updateDTO.Id);
        if (user is null)
            throw new Exception("UserNotFound");

        user.Update(updateDTO.Email, updateDTO.FirstName, updateDTO.LastName);
        await _databaseContext.SaveChangesAsync();
        return true;
    }

    public async System.Threading.Tasks.Task DeleteAsync(long id)
    {
        var user = _databaseContext.Users.FirstOrDefault();
        if (user is null)
            throw new Exception("UserNotFound");
        await _userManager.DeleteAsync(user);
    }
}
