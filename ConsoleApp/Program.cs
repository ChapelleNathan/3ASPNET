// See https://aka.ms/new-console-template for more information

using _3ASP;
using _3ASP.route;

var consoleMessages = new ConsoleMessages();

LaunchApp();


void LaunchApp()
{
    Console.WriteLine("Que voulez vous faire ?");
    Console.WriteLine("User: Accéder à toutes les commandes lié aux utilisateurs");
    Console.WriteLine("q: pour quitter la ligne de commande");
    var choice = Console.ReadLine();
    switch (choice)
    {
        case "User":
            UserCase();
            break;
        case "q":
            return;
        default:
            Console.WriteLine("Commande non trouvée");
            break;
    }

    LaunchApp();
}

async void UserCase()
{
    consoleMessages.UserMessage();
    var request = Console.ReadLine();
    switch (request)
    {
        case "GetAll":
            await UserHandler.GetUsers();
            break;
        case "GetOne":
            throw new NotImplementedException();
            break;
        case "AddOne":
            throw new NotImplementedException();
            break;
        case "UpdateOne":
            throw new NotImplementedException();
            break;
        case "DeleteOne":
            throw new NotImplementedException();
            break;
        case "return":
            break;
        default:
            Console.WriteLine("Commande non trouvée");
            UserCase();
            break;
    }
}
