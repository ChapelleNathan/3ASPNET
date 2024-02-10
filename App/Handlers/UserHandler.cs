using System.Net.Http.Json;
using System.Text.Json.Nodes;
using _3ASP;
using _3ASP.DTO.UserDto;
using _3ASP.Enums;
using Newtonsoft.Json.Linq;

namespace ConsoleApp.Handlers;

public static class UserHandler
{
    private const string url = "http://localhost:5243/api/User";

    public static async Task<List<UserDto>?> GetUsers()
    {
        using (HttpClient client = new HttpClient())
        {
            HttpResponseMessage response = await client.GetAsync(url);
            dynamic responseBody = JObject.Parse(await response.Content.ReadAsStringAsync());
            if (responseBody.success == "false")
            {
                ConsoleMessages.DisplayError(response.StatusCode, responseBody.message);
                return null;
            }

            Console.WriteLine(responseBody.data);
            List<UserDto> users = new List<UserDto>();
            foreach (var user in responseBody.data)
            {
                users.Add(new UserDto()
                {
                    Id = user.id,
                    Email = user.email,
                    Pseudo = user.pseudo,
                    Password = user.password,
                    Role = user.role,
                });
            }

            return users;
        }
    }

    public static async Task<UserDto?> GetOneUser(string userId)
    {
        using (HttpClient client = new HttpClient())
        {
            HttpResponseMessage response = await client.GetAsync(url + "/" + userId);

            dynamic responseBody = JObject.Parse(await response.Content.ReadAsStringAsync())!;
            if (responseBody.success == "false")
            {
                ConsoleMessages.DisplayError(response.StatusCode, responseBody.message);
                return null;
            }


            return new UserDto()
            {
                Id = responseBody.data.id,
                Email = responseBody.data.email,
                Pseudo = responseBody.data.pseudo,
                Password = responseBody.data.password,
                Role = responseBody.data.role,
            };
        }
    }

    public static async Task<UserDto?> AddOneUser()
    {
        PostUserDto userDto = ConsoleMessages.AddUserConsole();
        using (HttpClient client = new HttpClient())
        {
            HttpResponseMessage response = await client.PostAsJsonAsync(url, userDto);
            dynamic responseBody = JObject.Parse(await response.Content.ReadAsStringAsync());
            if (responseBody.success == "false")
            {
                ConsoleMessages.DisplayError(response.StatusCode, responseBody.message);
                return null;
            }
            
            return new UserDto()
            {
                Id = responseBody.data.id,
                Email = responseBody.data.email,
                Pseudo = responseBody.data.pseudo,
                Password = responseBody.data.password,
                Role = responseBody.data.role,
            };
        }
    }

    public static async Task<UserDto?> UpdateOneUser(string id)
    {
        UserDto? user = await GetOneUser(id);
        if (user is null) return null;
        UpdateUserDto updatedUser = ConsoleMessages.UpdateUserConsole(user);
        using (HttpClient client = new HttpClient())
        {
            HttpResponseMessage response = await client.PutAsJsonAsync(url, updatedUser);
            dynamic responseBody = JObject.Parse(await response.Content.ReadAsStringAsync());
            if (responseBody.success is null)
            {
                ConsoleMessages.DisplayError(response.StatusCode, responseBody.message);
                return null;
            }

            return new UserDto()
            {
                Id = responseBody.data.id,
                Password = responseBody.data.password,
                Pseudo = responseBody.data.pseudo,
                Email = responseBody.data.email,
                Role = responseBody.data.role,
            };
        }
    }
}