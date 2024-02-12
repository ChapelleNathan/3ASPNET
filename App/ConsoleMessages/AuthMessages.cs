using App.DTO.UserDto;

namespace App.ConsoleMessages;

public class AuthMessages : GenericMessages
{
    public static void AuthMessage()
    {
        Console.WriteLine("Que voulez vous faire ?");
        Console.WriteLine("Register: Créer un compte");
        Console.WriteLine("Login: Se connecter");
        Console.WriteLine("return: retour en arrière");
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

}