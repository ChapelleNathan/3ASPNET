using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using _3ASP.DTO.ProductDto;
using _3ASP.DTO.UserDto;
using Microsoft.IdentityModel.Tokens;

namespace _3ASP;

public class AutoMapperProfile : Profile
{
    public AutoMapperProfile()
    {
        CreateMap<User, UserDto>();
        CreateMap<PostUserDto, User>();
        CreateMap<Product, ProductDto>();
        //CreateMap<User, ConnectedUserDto>().ConvertUsing<UserToConnectedUser>();
    }
}

/*public class UserToConnectedUser : ITypeConverter<User, ConnectedUserDto>
{
    private readonly IConfiguration _configuration;

    protected UserToConnectedUser(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public ConnectedUserDto Convert(User source, ConnectedUserDto destination, ResolutionContext context)
    {
        var connectedUserDto = new ConnectedUserDto()
        {
            Email = source.Email,
            Pseudo = source.Pseudo,
            Token = CreateToken(source),
        };

        return connectedUserDto;
    }

    private string CreateToken(User user)
    {
        var claims = new List<Claim>
        {
            new Claim(ClaimTypes.Name, user.Pseudo),
            new Claim(ClaimTypes.Role, user.Role.ToString()),
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
}*/