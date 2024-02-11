using System.Diagnostics;
using _3ASP.Data;
using _3ASP.DTO.UserDto;
using _3ASP.Models;

namespace _3ASP.Services.UserServices;

public class UserService : IUserService
{
    private readonly IMapper _mapper;
    private readonly DataContext _context;

    public UserService(IMapper mapper, DataContext context)
    {
        _mapper = mapper;
        _context = context;
    }

    public async Task<ServiceResponse<List<UserDto>>> GetAllUsers()
    {
        var serviceResponse = new ServiceResponse<List<UserDto>>();
        var dbUsers = await _context.Users.ToListAsync();
        serviceResponse.Data = dbUsers.Select(u => _mapper.Map<UserDto>(u)).ToList()!;
        return serviceResponse;
    }

    public async Task<ServiceResponse<UserDto>> GetUserById(int id)
    {
        var serviceResponse = new ServiceResponse<UserDto>();
        try
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Id == id);
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
    
    public async Task<ServiceResponse<UserDto>> UpdateUser(UpdateUserDto updatedUser)
    {
        var serviceResponse = new ServiceResponse<UserDto>();
        try
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Id == updatedUser.Id);
            if (user is null)
            {
                throw new Exception("User not found");
            }

            user.Email = updatedUser.Email;
            user.Pseudo = updatedUser.Pseudo;
            _context.Users.Update(user);
            await _context.SaveChangesAsync();
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
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Id == id);
            if (user is null)
            {
                throw new Exception("User not found");
            }

            serviceResponse.Data = _mapper.Map<UserDto>(user);
            _context.Users.Remove(user);
            await _context.SaveChangesAsync();
        }
        catch (Exception e)
        {
            serviceResponse.Success = false;
            serviceResponse.Message = e.Message;
        }

        return serviceResponse;
    }
}