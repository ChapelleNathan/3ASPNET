using System.Net;

namespace App.ConsoleMessages;

public class GenericMessages
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
        Console.WriteLine("Auth: Se connecter ou créer un compte");
        Console.WriteLine("q: pour quitter la ligne de commande");
    }

    protected static string EntryTest()
    {
        string entry = Console.ReadLine()!;
        while (entry is "" or null)
        {
            Console.WriteLine("Entrée incorrect, veuillez recommancer");
            entry = Console.ReadLine()!;
        }

        return entry;
    }

    protected static string YesNo(string operation)
    {
        Console.WriteLine("Changement de : " + operation);
        Console.WriteLine("o: oui (default)");
        Console.WriteLine("n: non");
        var response = Console.ReadLine();
        if (response is "" or null)
        {
            response = "o";
        }

        return response;
    }
    
    public static void DisplayError(HttpStatusCode errorCode, dynamic message)
    {
        Console.WriteLine($"Une erreur est apparu avec le code {errorCode}");
        Console.WriteLine($"Erreur: {message}");
    }
}