using _3ASP.Enums;

namespace _3ASP.Models;

public class User
{
    public int Id { get; private set; }

    public string Email { get; private set; }

    public string Pseudo { get; private set; }

    public string Password { get; private set; }

    public Roles Role { get; private set; } = Roles.User;
}