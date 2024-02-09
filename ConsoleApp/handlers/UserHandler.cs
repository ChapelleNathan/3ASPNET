using System.Text.Json.Nodes;

namespace _3ASP.route;

public static class UserHandler
{
    public static  async Task GetUsers()
    {
        using (HttpClient client = new HttpClient())
        {
            string url = "http://localhost:5243/api/User";

            HttpResponseMessage @response = await client.GetAsync(url);
            if (response.IsSuccessStatusCode)
            {
                dynamic responseBody = JsonNode.Parse(await response.Content.ReadAsStringAsync()) ?? throw new InvalidOperationException();
                Console.WriteLine(responseBody);
            }
            else
            {
                Console.WriteLine($"Request failed with status code {response.StatusCode}");
            }
        }
    }
}