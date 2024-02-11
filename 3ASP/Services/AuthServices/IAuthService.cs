using _3ASP.DTO.UserDto;

namespace _3ASP.Services.AuthServices;

public interface IAuthService
{
    public Task<ServiceResponse<UserDto>> Register(PostUserDto user);

    public Task<ServiceResponse<UserDto>> LogIn(AuthUserDto user);
}