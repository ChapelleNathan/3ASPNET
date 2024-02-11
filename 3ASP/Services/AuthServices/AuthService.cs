using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using _3ASP.Data;
using _3ASP.DTO.UserDto;
using _3ASP.Enums;
using Microsoft.IdentityModel.Tokens;

namespace _3ASP.Services.AuthServices;

public class AuthService : IAuthService
{
    private readonly IMapper _mapper;
    private readonly DataContext _context;
    private readonly IConfiguration _configuration;

    public AuthService(IMapper mapper, DataContext context, IConfiguration configuration)
    {
        _mapper = mapper;
        _context = context;
        _configuration = configuration;
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
            //Je met deux messages différents entre la vérif d'un pseudo et de password pour tester les deux exceptions
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

    public async Task<ServiceResponse<string?>> Bearer(AuthUserDto request)
    {
        var serviceResponse = new ServiceResponse<string?>();
        User? user = await _context.Users.FirstOrDefaultAsync(u => u.Pseudo == request.Pseudo);
        if (user is null)
        {
            serviceResponse.Success = false;
            serviceResponse.Message = "User not found";
            return serviceResponse;
        }

        if (!BCrypt.Net.BCrypt.Verify(request.Password, user.Password))
        {
            serviceResponse.Success = false;
            serviceResponse.Message = "Wrong Password";
        }

        var token = CreateToken(user);
        serviceResponse.Data = token;
        return serviceResponse;
    }

    private string CreateToken(User user)
    {
        var claims = new List<Claim>
        {
            new Claim(ClaimTypes.Name, user.Pseudo),
            new Claim(ClaimTypes.Role, user.Role.ToString())
        };
        
        var key = new SymmetricSecurityKey(
            Encoding.UTF8.GetBytes(_configuration.GetSection("AppSettings:Token").Value!));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);
        var token = new JwtSecurityToken(
            claims: claims,
            expires: DateTime.Now.AddDays(1),
            signingCredentials: creds
        );
        
        var jwt = new JwtSecurityTokenHandler().WriteToken(token);

        return jwt;
    }
}