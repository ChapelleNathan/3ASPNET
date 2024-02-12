// See https://aka.ms/new-console-template for more information

using System.Text.Json;
using App;
using App.Choices;
using App.ConsoleMessages;
using Newtonsoft.Json.Linq;

var app = true;
while (app == true)
{
    dynamic? response = null;
    GenericMessages.Startup();
    var choice = Console.ReadLine();
    switch (choice)
    {
        case "Auth":
            response = await AuthChoice.AuthCase();
            if (response != null)
                Console.WriteLine(response);
            GenericMessages.EndOfOperation();
            break;
        case "User":
            response = await UserChoice.UserCase();
            if (response != null)
                Console.WriteLine(response);
            GenericMessages.EndOfOperation();
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


