using System.Text.Json.Nodes;
using _3ASP;
using Newtonsoft.Json.Linq;

namespace ConsoleApp.Handlers;

public static class UserHandler
{
    public static async Task GetUsers()
    {
        using (HttpClient client = new HttpClient())
        {
            const string url = "http://localhost:5243/api/User";

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
            string url = "http://localhost:5243/api/User/" + userId;

            HttpResponseMessage response = await client.GetAsync(url);

            dynamic responseBody = JObject.Parse(await response.Content.ReadAsStringAsync())!;
            if (responseBody.success == "false")
            {
                ConsoleMessages.DisplayError(response.StatusCode, responseBody.message);
                return;
            }

            Console.WriteLine(responseBody.data);
        }
    }
}