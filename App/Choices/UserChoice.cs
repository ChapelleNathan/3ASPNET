using ConsoleApp.Handlers;
using Newtonsoft.Json.Linq;

namespace _3ASP.Choices;

public class UserChoice
{
    public static async Task<dynamic?> UserCase()
    {
        ConsoleMessages.UserMessage();
        var request = Console.ReadLine();
        dynamic? response = null;
        switch (request)
        {
            case "GetAll":
                response = await UserHandler.GetUsers();
                break;
            case "GetOne":
                Console.WriteLine("Enseignez l'ID de l'utilisateur que vous voulez rechercher");
                response = await UserHandler.GetOneUser(Console.ReadLine()!);
                if (response is not null)
                    response = JObject.FromObject(response);
                break;
            case "AddOne":
                response = await UserHandler.AddOneUser();
                if (response is not null)
                    response = JObject.FromObject(response);
                break;
            case "UpdateOne":
                Console.WriteLine("Enseignez l'ID de l'utilisateur que vous voulez modifier");
                response = await UserHandler.UpdateOneUser(Console.ReadLine()!);
                if (response is not null)
                    response = JObject.FromObject(response);
                break;
            case "DeleteOne":
                Console.WriteLine("Enseignez l'ID de l'utilisateur que vous voulez supprimer");
                response = await UserHandler.DeleteUser(Console.ReadLine()!);
                if (response is not null)
                    response = JObject.FromObject(response);
                break;
            case "return":
                break;
            default:
                Console.WriteLine("Commande non trouvée");
                await UserCase();
                break;
        }
    
        return response;
    }
}