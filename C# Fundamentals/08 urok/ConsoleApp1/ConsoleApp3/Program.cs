using System;
internal class Program
{
    private static void Main(string[] args)
    {
        int n = int.Parse(Console.ReadLine());
        decimal total = 0;

        for (int i = 0; i < n; i++)
        {
            decimal current = decimal.Parse(Console.ReadLine());
            total += current;
        }

        Console.WriteLine(total);



    }
}