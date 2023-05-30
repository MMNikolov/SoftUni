using System;
internal class Program
{
    private static void Main(string[] args)
    {
        int meters = int.Parse(Console.ReadLine());
        double kilometers = 0;

        kilometers = meters / 1000d;

        Console.WriteLine($"{kilometers:f2}");


    }
}