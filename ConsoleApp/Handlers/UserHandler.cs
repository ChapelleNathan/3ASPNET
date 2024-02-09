using System.Net.Http.Json;
using System.Text.Json.Nodes;
using _3ASP;
using _3ASP.DTO.UserDto;
using Newtonsoft.Json.Linq;

namespace ConsoleApp.Handlers;

public static class UserHandler
{
    private const string url = "http://localhost:5243/api/User";
    public static async Task GetUsers()
    {
        using (HttpClient client = new HttpClient())
        {
            HttpResponseMessage response = await client.GetAsync(url);
            dynamic responseBody = JObject.Parse(await response.Content.ReadAsStringAsync());
            if (responseBody.success == "false")
            {
                ConsoleMessages.DisplayError(response.StatusCode, responseBody.message);
                return;
            }

            Console.WriteLine(responseBody.data);
        }
    }

    public static async Task GetOneUser(string userId)
    {
        using (HttpClient client = new HttpClient())
        {
            HttpResponseMessage response = await client.GetAsync(url + "/" + userId);

            dynamic responseBody = JObject.Parse(await response.Content.ReadAsStringAsync())!;
            if (responseBody.success == "false")
            {
                ConsoleMessages.DisplayError(response.StatusCode, responseBody.message);
                return;
            }

            Console.WriteLine(responseBody.data);
        }
    }

    public static async Task AddOneUser()
    {
        PostUserDto userDto = ConsoleMessages.AddUserConsole();
        using (HttpClient client = new HttpClient())
        {
            HttpResponseMessage response = await client.PostAsJsonAsync(url, userDto);
            dynamic responseBody = JObject.Parse(await response.Content.ReadAsStringAsync());
            if (responseBody.success == "false")
            {
                ConsoleMessages.DisplayError(response.StatusCode, responseBody.message);
                return;
            }

            Console.WriteLine(responseBody.data);
        }
    }
}