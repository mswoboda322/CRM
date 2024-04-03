namespace Application.Features.Users.Models;
public class TokenInfo
{
    public string Token { get; set; }
    public DateTime TokenValidUntil { get; set; }
    public string RefreshToken { get; set; }
}
