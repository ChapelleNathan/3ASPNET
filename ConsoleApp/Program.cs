// See https://aka.ms/new-console-template for more information

using _3ASP;
using ConsoleApp.Handlers;

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
    ConsoleMessages.UserMessage();
    var request = Console.ReadLine();
    switch (request)
    {
        case "GetAll":
            await UserHandler.GetUsers();
            break;
        case "GetOne":
            Console.WriteLine("Enseignez l'ID de l'utilisateur que vous voulez rechercher");
            string? userId = Console.ReadLine();
            while (userId is null)
            {
                Console.WriteLine("Veuillez enseignez un ID");
                userId = Console.ReadLine();
            }
            await UserHandler.GetOneUser(userId);
            break;
        case "AddOne":
            await UserHandler.AddOneUser();
            break;
        case "UpdateOne":
            throw new NotImplementedException();
        case "DeleteOne":
            throw new NotImplementedException();
        case "return":
            break;
        default:
            Console.WriteLine("Commande non trouvée");
            UserCase();
            break;
    }
}
