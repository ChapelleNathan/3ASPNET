using _3ASP.DTO.UserDto;
using _3ASP.Models;

namespace _3ASP.Services.UserServices;

public interface IUserService
{
    Task<ServiceResponse<List<UserDto>>> GetAllUsers();

    Task<ServiceResponse<UserDto>> GetUserById(int id);

    Task<ServiceResponse<List<UserDto>>> AddUser(PostUserDto user);

    Task<ServiceResponse<UserDto>> UpdateUser(UpdateUserDto updatedUser);
}