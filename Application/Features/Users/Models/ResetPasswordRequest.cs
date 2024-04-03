using System.ComponentModel.DataAnnotations;

namespace Application.Features.Users.Models;
public class ResetPasswordRequest
{
    [Required]
    public string Email { get; set; }
}
