using System.Net.Http.Json;
using App.ConsoleMessages;
using App.DTO.UserDto;
using Newtonsoft.Json.Linq;

namespace App.Handlers;

public static class UserHandler
{
    private const string Url = "http://localhost:5243/api/User";

    public static async Task<List<UserDto>?> GetUsers()
    {
        using HttpClient client = new HttpClient();
        HttpResponseMessage response = await client.GetAsync(Url);
        
        dynamic responseBody = JObject.Parse(await response.Content.ReadAsStringAsync());
        if (responseBody.success == "false")
        {
            UserMessages.DisplayError(response.StatusCode, responseBody.message);
            return null;
        }

        List<UserDto> users = new List<UserDto>();
        foreach (var user in responseBody.data)
        {
            users.Add(new UserDto()
            {
                Id = user.id,
                Email = user.email,
                Pseudo = user.pseudo,
                Role = user.role,
            });
        }

        return users;
    }

    public static async Task<UserDto?> GetOneUser(string userId)
    {
        using HttpClient client = new HttpClient();
        HttpResponseMessage response = await client.GetAsync(Url + "/" + userId);

        dynamic responseBody = JObject.Parse(await response.Content.ReadAsStringAsync())!;
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

    public static async Task<UserDto?> UpdateOneUser(string id)
    {
        UserDto? user = await GetOneUser(id);
        if (user is null) return null;
        
        UpdateUserDto updatedUser = UserMessages.UpdateUserConsole(user);
        
        using HttpClient client = new HttpClient();
        HttpResponseMessage response = await client.PutAsJsonAsync(Url, updatedUser);
        
        dynamic responseBody = JObject.Parse(await response.Content.ReadAsStringAsync());
        if (responseBody.success is null)
        {
            UserMessages.DisplayError(response.StatusCode, responseBody.message);
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

    public static async Task<UserDto?> DeleteUser(string id)
    {
        using HttpClient client = new HttpClient();
        HttpResponseMessage response = await client.DeleteAsync(Url + "/" + id);
        
        dynamic responseBody = JObject.Parse(await response.Content.ReadAsStringAsync());
        if (responseBody.success == "false")
        {
            UserMessages.DisplayError(response.StatusCode, responseBody.message);
            return null;
        }

        return new UserDto()
        {
            Id = responseBody.data.id,
            Pseudo = responseBody.data.pseudo,
            Role = responseBody.data.role,
            Email = responseBody.data.email,
        };
    }
}