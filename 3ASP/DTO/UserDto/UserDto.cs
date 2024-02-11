using _3ASP.Enums;

namespace _3ASP.DTO.UserDto;

public class UserDto
{
    public int Id { get; private set; }
    public required string Pseudo { get;  init; }
    public required string Email { get;  init; }
    public required string Password { get;  init; }
    public Roles Role { get; private set; } = Roles.User;
}