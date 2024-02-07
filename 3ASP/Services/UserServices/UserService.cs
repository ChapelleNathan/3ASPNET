using _3ASP.DTO.UserDto;
using _3ASP.Models;

namespace _3ASP.Services.UserServices;

public class UserService : IUserService
{
    private static List<User> _users = new List<User>
    {
        new User()
        {
            Id = 1, Pseudo = "Toto", Email = "toto@g.c", Password = BCrypt.Net.BCrypt.HashPassword("azerty")
        },
        new User()
        {
            Id = 2, Pseudo = "Tata", Email = "tata@g.c", Password = BCrypt.Net.BCrypt.HashPassword("tatatoto")
        }
    };

    public async Task<ServiceResponse<List<User>>> GetAllUsers()
    {
        var serviceResponse = new ServiceResponse<List<User>>();
        serviceResponse.Data = _users;
        return serviceResponse;
    }

    public async Task<ServiceResponse<User>> GetUserById(int id)
    {
        var user = _users.Find(u => u.Id == id);
        var serviceResponse = new ServiceResponse<User>();
        serviceResponse.Data = user;
        throw new Exception("User not found");
    }

    public async Task<ServiceResponse<List<User>>> AddUser(PostUserDto user)
    {
        var serviceResponse = new ServiceResponse<List<User>>();
        var newUser = new User()
        {
            Id = _users.Max(u => u.Id) + 1,
            Pseudo = user.Pseudo,
            Password = BCrypt.Net.BCrypt.HashPassword(user.Password),
            Email = user.Email
        };
            
        _users.Add(newUser);
        serviceResponse.Data = _users;

        return serviceResponse;
    }
}