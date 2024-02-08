using System.Diagnostics;
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

    private readonly IMapper _mapper;

    public UserService(IMapper mapper)
    {
        _mapper = mapper;
    }

    public async Task<ServiceResponse<List<UserDto>>> GetAllUsers()
    {
        var serviceResponse = new ServiceResponse<List<UserDto>>();
        serviceResponse.Data = _users.Select(u => _mapper.Map<UserDto>(u)).ToList()!;
        return serviceResponse;
    }

    public async Task<ServiceResponse<UserDto>> GetUserById(int id)
    {
        var serviceResponse = new ServiceResponse<UserDto>();
        try
        {
            var user = _users.Find(u => u.Id == id);
            if (user is null) throw new Exception("User not found");
            serviceResponse.Data = _mapper.Map<UserDto>(user);
        }
        catch (Exception e)
        {
            serviceResponse.Success = false;
            serviceResponse.Message = e.Message;
        }
        
        return serviceResponse;
    }

    public async Task<ServiceResponse<List<UserDto>>> AddUser(PostUserDto userDto)
    {
        var serviceResponse = new ServiceResponse<List<UserDto>>();
        var newUser = _mapper.Map<User>(userDto)!;
        newUser.Id = _users.Max(u => u.Id) + 1;
        newUser.Password = BCrypt.Net.BCrypt.HashPassword(userDto.Password);
        
        _users.Add(newUser);
        
        serviceResponse.Data = _users.Select(u => _mapper.Map<UserDto>(u)).ToList()!;
        return serviceResponse;
    }

    public async Task<ServiceResponse<UserDto>> UpdateUser(UpdateUserDto updatedUser)
    {
        var serviceResponse = new ServiceResponse<UserDto>();
        try
        {
            var user = _users.Find(u => u.Id == updatedUser.Id);
            if (user is null)
            {
                throw new Exception("User not found");
            }
            user.Email = updatedUser.Email;
            user.Pseudo = updatedUser.Pseudo;
            user.Password = BCrypt.Net.BCrypt.HashPassword(updatedUser.Password);
            
            serviceResponse.Data = _mapper.Map<UserDto>(user);
        }
        catch (Exception e)
        {
            serviceResponse.Success = false;
            serviceResponse.Message = e.Message;
        }

        return serviceResponse;
    }

    public async Task<ServiceResponse<UserDto>> DeleteUser(int id)
    {
        var serviceResponse = new ServiceResponse<UserDto>();
        try
        {
            var user = _users.Find(u => u.Id == id);
            if (user is null)
            {
                throw new Exception("User not found");
            }
            serviceResponse.Data = _mapper.Map<UserDto>(user);
            _users.Remove(user);
        }
        catch (Exception e)
        {
            serviceResponse.Success = false;
            serviceResponse.Message = e.Message;
        }

        return serviceResponse;
    }
}