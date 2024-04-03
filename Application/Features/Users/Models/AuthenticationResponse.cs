namespace Application.Features.Users.Models;
public class AuthenticationResponse
{
    public TokenInfo TokenData { get; set; }
    public AuthenticationResponse(TokenInfo tokenInfo)
    {
        TokenData = tokenInfo;
    }
}
