using Application.Features.Users.Models;
using Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security;
using System.Security.Claims;
using System.Security.Principal;
using System.Text;

namespace Application.Features.Users;
public class AuthenticationService : IAuthenticationService
{

    private readonly AuthenticationSettings _appSettings;
    private readonly SignInManager<User> _signInManager;
    private readonly UserManager<User> _userManager;
    public AuthenticationService(IOptions<AuthenticationSettings> appSettings,
                                SignInManager<User> signInManager,
                                UserManager<User> userManager)
    {
        _appSettings = appSettings.Value;
        _signInManager = signInManager;
        _userManager = userManager;
    }

    public async Task<AuthenticationResponse> Authenticate(AuthenticationRequest model)
    {
        var user = await GetUserEntity(model.Email);
        SignInResult loginResult;
        loginResult = await _signInManager.PasswordSignInAsync(user, model.Password, false, true);
        if (loginResult.Succeeded)
        {
            TokenInfo token = await GenerateTokenInfo(user);
            return new AuthenticationResponse(token);
        }
        if (loginResult.RequiresTwoFactor)
            throw new SecurityException("RequiresTwoFactor");

        if (loginResult.IsLockedOut)
            throw new SecurityException("IsLockedOut");
        
        throw new Exception("CannotLogin");
    }
   
    private async Task<TokenInfo> GenerateToken(string userName, User user, string refreshToken)
    {
        JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();
        byte[] key = Encoding.ASCII.GetBytes(_appSettings.Secret);
        IList<Claim> claims = await _userManager.GetClaimsAsync(user);
        IList<string> roles = await _userManager.GetRolesAsync(user);
        foreach (var role in roles)
        {
            claims.Add(new Claim(ClaimTypes.Role, role));
        }
        claims.Add(new Claim(Microsoft.IdentityModel.JsonWebTokens.JwtRegisteredClaimNames.Sub, userName));
        claims.Add(new Claim(ClaimTypes.Name, user.Email));
        claims.Add(new Claim("UserId", user.Id.ToString()));

        ClaimsIdentity claimIdentity = new ClaimsIdentity(new GenericIdentity(user.UserName, "Token"), claims);
        SecurityTokenDescriptor tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = claimIdentity,
            Expires = DateTime.UtcNow.AddMinutes(15),
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
        };
        SecurityToken token = tokenHandler.CreateToken(tokenDescriptor);
        return new TokenInfo { Token = tokenHandler.WriteToken(token), TokenValidUntil = tokenDescriptor.Expires.Value, RefreshToken = refreshToken };
    }
    private async Task<User> GetUserEntity(string name)
    {
        var user = await _userManager.FindByNameAsync(name);
        if (user == null)
            throw new Exception("UserNotFound");

        return user;
    }
    public async Task<TokenInfo> GenerateTokenInfo(User user)
    {
        await _userManager.RemoveAuthenticationTokenAsync(user, "loginProviderName", "RefreshToken");
        string refreshToken = GenerateRefreshToken(user.Id);
        TokenInfo token = await GenerateToken(user.Email, user, refreshToken);
        await _userManager.SetAuthenticationTokenAsync(user, "loginProviderName", "RefreshToken", refreshToken);
        return token;
    }
    private string GenerateRefreshToken(long userId)
    {
        JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();
        byte[] key = Encoding.ASCII.GetBytes(_appSettings.Secret);
        ClaimsIdentity claimIdentity = new ClaimsIdentity(new GenericIdentity(userId.ToString(), "UserId"));
        SecurityTokenDescriptor tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = claimIdentity,
            Expires = DateTime.UtcNow.AddDays(30),
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
        };
  
        SecurityToken token = tokenHandler.CreateToken(tokenDescriptor);
        return tokenHandler.WriteToken(token);
    }
}
