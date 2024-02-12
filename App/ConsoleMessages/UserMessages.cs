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
        Console.WriteLine("DeleteOne: Supprime un utilisateur selon un ID");
        Console.WriteLine("return: Retourner en arrière");
    }

    public static void DisplayError(HttpStatusCode errorCode, dynamic message)
    {
        Console.WriteLine($"Une erreur est apparu avec le code {errorCode}");
        Console.WriteLine($"Erreur: {message}");
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