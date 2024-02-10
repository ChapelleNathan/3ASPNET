// See https://aka.ms/new-console-template for more information

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
    


async Task<dynamic> UserCase()
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
            string userId = Console.ReadLine()!;
            response = JObject.FromObject(await UserHandler.GetOneUser(userId));
            break;
        case "AddOne":
            response = JObject.FromObject(await UserHandler.AddOneUser());
            break;
        case "UpdateOne":
            throw new NotImplementedException();
        case "DeleteOne":
            throw new NotImplementedException();
        case "return":
            break;
        default:
            Console.WriteLine("Commande non trouvée");
            await UserCase();
            break;
    }

    return response;
}