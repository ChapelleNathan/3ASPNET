namespace _3ASP;

public class ConsoleMessages
{
    public void UserMessage()
    {
        Console.WriteLine("Que voulez vous faire ?");
        Console.WriteLine("GetAll: Retourne tous les utilisateurs");
        Console.WriteLine("GetOne: Retourne un utilisateur selon un ID");
        Console.WriteLine("UpdateOne: Modifie un utilisateur");
        Console.WriteLine("AddOne: Créer un nouvel utilisateur");
        Console.WriteLine("DeleteOne: Supprime un utilisateur selon un ID");
    }
}