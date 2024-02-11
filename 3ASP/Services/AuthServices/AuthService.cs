using _3ASP.Data;
using _3ASP.DTO.UserDto;

namespace _3ASP.Services.AuthServices;

public class AuthService : IAuthService
{
    private readonly IMapper _mapper;
    private readonly DataContext _context;

    public AuthService(IMapper mapper, DataContext context)
    {
        _mapper = mapper;
        _context = context;
    }

    public async Task<ServiceResponse<UserDto>> Register(PostUserDto user)
    {
        var serviceResponse = new ServiceResponse<UserDto>();
        var newUser = _mapper.Map<User>(user)!;
        newUser.Password = BCrypt.Net.BCrypt.HashPassword(newUser.Password);
        try
        {
            await _context.Users.AddAsync(newUser);
            await _context.SaveChangesAsync();
            serviceResponse.Data = _mapper.Map<UserDto>(newUser);
        }
        catch (Exception e)
        {
            serviceResponse.Success = false;
            serviceResponse.Message = "Pseudo already taken";
        }

        return serviceResponse;
    }

    public async Task<ServiceResponse<UserDto>> LogIn(AuthUserDto request)
    {
        var serviceResponse = new ServiceResponse<UserDto>(); 
        try
        {
            User? user = await _context.Users.FirstOrDefaultAsync(u => u.Pseudo == request.Pseudo);
            if (user is null) throw new Exception("User not found");
            try
            {
                if (!BCrypt.Net.BCrypt.Verify(request.Password, user.Password))
                {
                    throw new Exception("Wrong password or username");
                }

                serviceResponse.Data = _mapper.Map<UserDto>(user);
            }
            catch (Exception e)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = e.Message;
            }
        }
        catch (Exception e)
        {
            serviceResponse.Success = false;
            serviceResponse.Message = e.Message;
        }
        
        return serviceResponse;
    }
}