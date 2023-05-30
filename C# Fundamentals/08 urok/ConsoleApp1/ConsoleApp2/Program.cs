using System;
internal class Program
{
    private static void Main(string[] args)
    {
        double pounds = double.Parse(Console.ReadLine());
        double dollars = pounds * 1.31;
        Console.WriteLine($"{dollars:f3}");


    }
}