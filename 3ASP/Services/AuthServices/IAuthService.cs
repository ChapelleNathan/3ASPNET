using _3ASP.DTO.UserDto;

namespace _3ASP.Services.AuthServices;

public interface IAuthService
{
    public Task<ServiceResponse<UserDto>> Register(PostUserDto user);

    public Task<ServiceResponse<UserDto>> LogIn(AuthUserDto user);

    //For test only, can get a bearer token from here
    public Task<ServiceResponse<string?>> Bearer(AuthUserDto user);
}