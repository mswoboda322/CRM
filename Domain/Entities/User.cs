using Microsoft.AspNetCore.Identity;

namespace Domain.Entities;
public class User : IdentityUser<long>
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public IList<Task> Tasks { get; set; }

    public void Update(string email, string firstName, string lastName)
    {
        Email = email;
        FirstName = firstName;
        LastName = lastName;
    }

    public static class Factory
    {
        public static User Create(string email, string firstName, string lastName)
        {
            var user = new User();
            user.Email = email;
            user.UserName = email;
            user.FirstName = firstName;
            user.LastName = lastName;
            user.EmailConfirmed = true;
            return user;
        }
    }
}
