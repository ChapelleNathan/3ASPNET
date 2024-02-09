using _3ASP.Enums;

namespace _3ASP.DTO.UserDto;

public class UserDto
{
    public int Id { get;  set; }
    public string Pseudo { get;  set; }
    public string Email { get;  set; }
    public string Password { get;  set; }
    public Roles Role { get;  set; } = Roles.User;
}