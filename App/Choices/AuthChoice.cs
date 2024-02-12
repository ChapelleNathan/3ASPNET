using App.ConsoleMessages;
using App.DTO.UserDto;
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
        string? pseudo;
        string? password;
        switch (choice)
        {
            case "Register":
                response = await AuthHandler.Register();
                if (response is not null)
                    response = JObject.FromObject(response);
                break;
            case "Login":
                Console.WriteLine("Pseudo ?");
                pseudo = Console.ReadLine()!;
                Console.WriteLine("Password ?");
                password = Console.ReadLine()!;
                response = await AuthHandler.Login(new AuthUserDto() { Pseudo = pseudo, Password = password });
                if (response is not null)
                    response = JObject.FromObject(response);
                break;
            case "jwt":
                Console.WriteLine("Pseudo ?");
                pseudo = Console.ReadLine()!;
                Console.WriteLine("Password ?");
                password = Console.ReadLine()!;
                response = await AuthHandler.Jwt(new AuthUserDto() { Password = password, Pseudo = pseudo });
                Console.WriteLine(response);
                break;
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