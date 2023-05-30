using System;
internal class Program
{
    private static void Main(string[] args)
    {
        int a = int.Parse(Console.ReadLine());
        double b = a * 0.70;
        double c = b * 0.85;
        double d = c / 2;

        Console.WriteLine($"{a + b + c + d:f2}");

    }
}