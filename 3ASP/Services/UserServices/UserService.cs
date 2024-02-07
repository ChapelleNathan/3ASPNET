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
        serviceResponse.Data = _users.Select(c => _mapper.Map<UserDto>(c)).ToList()!;
        return serviceResponse;
    }

    public async Task<ServiceResponse<UserDto>> GetUserById(int id)
    {
        var user = _users.Find(u => u.Id == id);
        if (user == null) throw new Exception("User not found");
        var serviceResponse = new ServiceResponse<UserDto>();
        serviceResponse.Data = _mapper.Map<UserDto>(user);
        return serviceResponse;
    }

    public async Task<ServiceResponse<List<UserDto>>> AddUser(PostUserDto userDto)
    {
        var newUser = _mapper.Map<User>(userDto) ?? throw new Exception();
        newUser.Id = _users.Max(c => c.Id) + 1;
        newUser.Password = BCrypt.Net.BCrypt.HashPassword(userDto.Password);
        var serviceResponse = new ServiceResponse<List<UserDto>>();
        _users.Add(newUser);
        serviceResponse.Data = _users.Select(c => _mapper.Map<UserDto>(c)).ToList()!;

        return serviceResponse;
    }
}