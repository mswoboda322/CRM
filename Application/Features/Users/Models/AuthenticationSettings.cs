namespace Application.Features.Users.Models;
public class AuthenticationSettings
{
    public bool Development { get; set; }
    public string? Secret { get; set; }
    public IEnumerable<string> Issuers { get; set; } = new List<string>();
    public IEnumerable<string> Audiences { get; set; } = new List<string>();
}
