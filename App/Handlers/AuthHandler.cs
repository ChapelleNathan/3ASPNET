using System.Net.Http.Json;
using App.ConsoleMessages;
using App.DTO.UserDto;
using Newtonsoft.Json.Linq;

namespace App.Handlers;

public static class AuthHandler
{
    private  const string Url = "http://localhost:5243/api/Authentication";
        
    public static async Task<UserDto?> Register()
    {
        PostUserDto userDto = AuthMessages.AddUserConsole();
        
        using HttpClient client = new HttpClient();
        HttpResponseMessage response = await client.PostAsJsonAsync(Url + "/register", userDto);
        
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

    public static async Task<UserDto?> Login(AuthUserDto request)
    {
        using HttpClient client = new HttpClient();
        HttpResponseMessage response = await client.PostAsJsonAsync(Url + "/login", request);
        dynamic responseBody = JObject.Parse(await response.Content.ReadAsStringAsync());
        if (responseBody.success == "false")
        {
            GenericMessages.DisplayError(response.StatusCode, responseBody.message);
            return null;
        }

        return new UserDto()
        {
            Id = responseBody.data.id,
            Pseudo = responseBody.data.pseudo,
            Email = responseBody.data.email,
            Role = responseBody.data.role,
        };
    }

    public static async Task<string?> Jwt(AuthUserDto request)
    {
        using HttpClient client = new HttpClient();
        HttpResponseMessage response = await client.PostAsJsonAsync(Url, request);
        dynamic responseBody = JObject.Parse(await response.Content.ReadAsStringAsync());
        if (responseBody.success == "false")
        {
            GenericMessages.DisplayError(response.StatusCode, responseBody.message);
            return null;
        }
        return responseBody.data;
    }
}