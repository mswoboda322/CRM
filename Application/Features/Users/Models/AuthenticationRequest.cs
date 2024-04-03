using System.ComponentModel.DataAnnotations;

namespace Application.Features.Users.Models;
public class AuthenticationRequest
{
    [Required]
    public string Email { get; set; }

    [Required]
    public string Password { get; set; }
}
