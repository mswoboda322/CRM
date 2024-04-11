namespace Application.Features.Users.DTOs;
public class UserUpdateDTO
{
    public long Id { get; set; }
    public string Email { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }

    public UserUpdateDTO(long id, string email, string firstName, string lastName)
    {
        Id = id;
        Email = email;
        FirstName = firstName;
        LastName = lastName;
    }
}
