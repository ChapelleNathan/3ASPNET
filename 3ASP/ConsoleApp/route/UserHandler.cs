namespace _3ASP.route;

public class UserHandler
{
    public  void handler(string request)
    {
        switch (request)
        {
            case "getAll":
                GetUsers();
                break;
        }
    }
    private  async Task GetUsers()
    {
        using (HttpClient client = new HttpClient())
        {
            string url = "http://localhost:5243/api/User";

            HttpResponseMessage response = await client.GetAsync(url);
            if (response.IsSuccessStatusCode)
            {
                string responseBody = await response.Content.ReadAsStringAsync();
                Console.WriteLine(responseBody);
            }
            else
            {
                Console.WriteLine($"Request failed with status code {response.StatusCode}");
            }
        }
    }
}