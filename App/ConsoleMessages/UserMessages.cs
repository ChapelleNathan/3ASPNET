using System.Net;
using App.DTO.UserDto;

namespace App.ConsoleMessages;

public abstract class UserMessages : GenericMessages
{
    public static void UserMessage()
    {
        Console.WriteLine("Que voulez vous faire ?");
        Console.WriteLine("GetAll: Retourne tous les utilisateurs");
        Console.WriteLine("GetOne: Retourne un utilisateur selon un ID");
        Console.WriteLine("UpdateOne: Modifie un utilisateur");
        Console.WriteLine("AddOne: Créer un nouvel utilisateur");
        Console.WriteLine("DeleteOne: Supprime un utilisateur selon un ID");
        Console.WriteLine("return: Retourner en arrière");
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
}