using System.ComponentModel.DataAnnotations;

namespace Application.Features.Users.Models;
public class ConfirmPasswordResetRequest
{
    public string Code { get; set; }
    public string Email { get; set; }
    public string NewPassword { get; set; }
}
