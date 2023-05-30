using System;
internal class Program
{
    private static void Main(string[] args)
    {
        string username = Console.ReadLine();
        string password = Console.ReadLine();
        string input = Console.ReadLine();

        while (input != password)
        {
            input = Console.ReadLine();

        }

        Console.WriteLine($"Welcome {username}!");

    }
}