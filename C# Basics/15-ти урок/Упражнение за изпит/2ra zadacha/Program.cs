using System;
using System.Numerics;

internal class Program
{
    private static void Main(string[] args)
    {
        int a = int.Parse(Console.ReadLine());
        int b = int.Parse(Console.ReadLine());
        int c = int.Parse(Console.ReadLine());
        double d = a - (a * 0.85);
        int e = b * c;
        double f = d + e;

        if (a >= f)
        {
            Console.WriteLine($"You managed to finish the movie on time! You have {Math.Round(a - f)} minutes left!");
        }
        else 
        {
            Console.WriteLine($"Time is up! To complete the movie you need {Math.Round(f - a)} minutes.");
        }


    }
}