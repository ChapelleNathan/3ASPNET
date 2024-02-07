using _3ASP.DTO.UserDto;
using _3ASP.Models;

namespace _3ASP.Services.UserServices;

public interface IUserService
{
    List<User> GetAllUsers();

    User GetUserById(int id);

    List<User> AddUser(PostUserDto user);
}