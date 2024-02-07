using _3ASP.DTO.UserDto;
using _3ASP.Models;

namespace _3ASP.Services.UserServices;

public interface IUserService
{
    Task<ServiceResponse<List<User>>> GetAllUsers();

    Task<ServiceResponse<User>> GetUserById(int id);

    Task<ServiceResponse<List<User>>> AddUser(PostUserDto user);
}