using App.ConsoleMessages;
using App.Handlers;
using Newtonsoft.Json.Linq;

namespace App.Choices;

public class AuthChoice
{
    public static async Task<dynamic?> AuthCase()
    {
        AuthMessages.AuthMessage();
        var choice = Console.ReadLine();
        dynamic? response = null;
        switch (choice)
        {
            case "Register":
                response = await AuthHandler.Register();
                if (response is not null)
                    response = JObject.FromObject(response);
                break;
            case "Login":
                throw new NotImplementedException();
            case "return":
                break;
            default:
                Console.WriteLine("Choix non trouvé");
                await AuthCase();
                break;
        }
        return response;
    }

}