using _3ASP.route;

namespace _3ASP;

public class ConsoleApp
{
    private readonly ConsoleMessages _consoleMessages;
    public ConsoleApp(ConsoleMessages consoleMessages)
    {
        _consoleMessages = consoleMessages;
    }

    public void LaunchApp()
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

    private void UserCase()
    {
        _consoleMessages.UserMessage();
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
}