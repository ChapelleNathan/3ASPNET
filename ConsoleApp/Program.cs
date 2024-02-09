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
    }

    LaunchApp();
}

void UserCase()
{
    consoleMessages.UserMessage();
    var userHandler = new UserHandler();
    var request = Console.ReadLine();
    if (request is null)
    {
        Console.WriteLine("Je n'ai pas compris, que voulez vous faire ?");
        UserCase();
    }
    else
    {
        userHandler.handler(request);
    }
}
