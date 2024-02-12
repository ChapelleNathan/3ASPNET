using System.Net.Http.Json;
using App.ConsoleMessages;
using App.DTO.UserDto;
using Newtonsoft.Json.Linq;

namespace App.Handlers;

public static class AuthHandler
{
    private  const string Url = "http://localhost:5243/api/Authentication/register";
        
    public static async Task<UserDto?> Register()
    {
        PostUserDto userDto = AuthMessages.AddUserConsole();
        
        using HttpClient client = new HttpClient();
        HttpResponseMessage response = await client.PostAsJsonAsync(Url, userDto);
        
        dynamic responseBody = JObject.Parse(await response.Content.ReadAsStringAsync());
        if (responseBody.success == "false")
        {
            UserMessages.DisplayError(response.StatusCode, responseBody.message);
            return null;
        }
            
        return new UserDto()
        {
            Id = responseBody.data.id,
            Email = responseBody.data.email,
            Pseudo = responseBody.data.pseudo,
            Role = responseBody.data.role,
        };
    }
}