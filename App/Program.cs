// See https://aka.ms/new-console-template for more information

using System.Text.Json;
using _3ASP;
using _3ASP.DTO.UserDto;
using ConsoleApp.Handlers;
using Newtonsoft.Json.Linq;

var app = true;
while (app == true)
{
    ConsoleMessages.Startup();
    var choice = Console.ReadLine();
    switch (choice)
    {
        case "User":
            var response = await UserCase();
            if (response != null)
                Console.WriteLine(response);
            ConsoleMessages.EndOfOperation();
            break;
        case "q":
            app = false;
            Console.WriteLine("Au revoir !");
            break;
        default:
            Console.WriteLine("Commande non trouvée");
            break;
    }
}


async Task<dynamic?> UserCase()
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