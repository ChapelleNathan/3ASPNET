using System.Net;
using _3ASP.DTO.UserDto;

namespace _3ASP;

public static class ConsoleMessages
{
    public static void EndOfOperation()
    {
        Console.WriteLine("Appuyer sur n'importe quel touche pour continuer");
        Console.ReadLine();
    }
    public static void Startup()
    {
        Console.WriteLine("Que voulez vous faire ?");
        Console.WriteLine("User: Accéder à toutes les commandes lié aux utilisateurs");
        Console.WriteLine("q: pour quitter la ligne de commande");
    }
    public static void UserMessage()
    {
        Console.WriteLine("Que voulez vous faire ?");
        Console.WriteLine("GetAll: Retourne tous les utilisateurs");
        Console.WriteLine("GetOne: Retourne un utilisateur selon un ID");
        Console.WriteLine("UpdateOne: Modifie un utilisateur");
        Console.WriteLine("AddOne: Créer un nouvel utilisateur");
        Console.WriteLine("DeleteOne: Supprime un utilisateur selon un ID");
    }

    public static void DisplayError(HttpStatusCode errorCode, dynamic message)
    {
        Console.WriteLine($"Une erreur est apparu avec le code {errorCode}");
        Console.WriteLine($"Erreur: {message}");
    }

    public static PostUserDto AddUserConsole()
    {
        Console.WriteLine("Pseudo ?");
        string pseudo = Console.ReadLine()!;
        Console.WriteLine("Email ?");
        string email = Console.ReadLine()!;
        Console.WriteLine("Mot de passe ?");
        string password = Console.ReadLine()!;

        while (pseudo == "")
        {
            Console.WriteLine("Il faut un pseudo valable");
            pseudo = Console.ReadLine()!;
        }

        while (email == "")
        {
            Console.WriteLine("Il faut un elauk valable");
            email = Console.ReadLine()!;
        }

        while (password == "")
        {
            Console.WriteLine("Il faut un mot de passe valable");
            password = Console.ReadLine()!;
        }

        PostUserDto newUser = new PostUserDto()
        {
            Pseudo = pseudo,
            Email = email,
            Password = password
        };

        return newUser;
    }

    public static UpdateUserDto UpdateUserConsole(UserDto updatedUser)
    {
        UpdateUserDto user = new UpdateUserDto()
        {
            Id = updatedUser.Id,
            Pseudo = updatedUser.Pseudo,
            Email = updatedUser.Email,
        };
        
        string response = YesNo("pseudo");
        if (response == "o")
        {
            Console.WriteLine("Nouveau pseudo ?");
            user.Pseudo = EntryTest();
        }

        response = YesNo("email");
        if (response is "o")
        {
            Console.WriteLine("Nouvelle email ?");
            user.Email = EntryTest();
        }
        
        return user;
    }

    private static string EntryTest()
    {
        string entry = Console.ReadLine()!;
        while (entry is "" or null)
        {
            Console.WriteLine("Entrée incorrect, veuillez recommancer");
            entry = Console.ReadLine()!;
        }
        return entry;
    } 

    private static string YesNo(string operation)
    {
        Console.WriteLine("Changement de : " + operation);
        Console.WriteLine("o: oui (default)");
        Console.WriteLine("n: non");
        var response = Console.ReadLine();
        if (response is "" or null )
        {
            response = "o";
        }
        return response;
    }
}