using System;
internal class Program
{
    private static void Main(string[] args)
    {
        string a = Console.ReadLine();
        int b = int.Parse(Console.ReadLine());
        int c = int.Parse(Console.ReadLine());

        double d = 0.0;

        if (a == "Premiere")
        {
            d = b * c * 12.00;
        }
        else if (a == "Normal")
        {
            d = b * c * 7.50;
        }
        else if (a == "Discount")
        {
            d = b * c * 5.00;
        }
        Console.WriteLine($"{d} leva");
    }
}