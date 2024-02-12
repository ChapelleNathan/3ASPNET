namespace App.DTO.UserDto;

public class ConnectedUserDto
{
    public string Token { get; set; } = string.Empty;
    public required string Pseudo { get; init; } = string.Empty;
    public required string Email { get;  init; }
}