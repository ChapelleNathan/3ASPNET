// See https://aka.ms/new-console-template for more information

using System.Text.Json;
using _3ASP;
using _3ASP.Choices;
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
            var response = await UserChoice.UserCase();
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


