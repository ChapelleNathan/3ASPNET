using System.Diagnostics;
using System.Security.Claims;
using _3ASP.Data;
using _3ASP.DTO.UserDto;
using _3ASP.Models;

namespace _3ASP.Services.UserServices;

public class UserService : IUserService
{
    private readonly IMapper _mapper;
    private readonly DataContext _context;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public UserService(IMapper mapper, DataContext context, IHttpContextAccessor httpContextAccessor)
    {
        _mapper = mapper;
        _context = context;
        _httpContextAccessor = httpContextAccessor;
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
        if (!VerifyUser(updatedUser.Id.ToString()))
        {
            serviceResponse.Success = false;
            serviceResponse.Message = "Unauthorized";
            return serviceResponse;
        }
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
        if (!VerifyUser(id.ToString()))
        {
            serviceResponse.Success = false;
            serviceResponse.Message = "Unauthorized";
            return serviceResponse;
        }
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

    private bool VerifyUser(string id)
    {
        if (_httpContextAccessor.HttpContext is null)
        {
            return false;
        }
        var result = _httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);

        return result == id;
    }
}