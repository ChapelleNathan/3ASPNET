using _3ASP.DTO.UserDto;

namespace _3ASP;

public class AutoMapperProfile : Profile
{
    public AutoMapperProfile()
    {
        CreateMap<User, UserDto>();
        CreateMap<PostUserDto, User>();
    }
}