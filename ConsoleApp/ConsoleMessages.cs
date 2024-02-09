using System.Net;

namespace _3ASP;

public static class ConsoleMessages
{
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
}