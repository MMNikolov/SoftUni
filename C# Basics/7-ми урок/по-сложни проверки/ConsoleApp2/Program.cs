using System;
internal class Program
{
    private static void Main(string[] args)
    {
        double number = double.Parse(Console.ReadLine());

        if (number >= -100 && number <= 100 && number != 0)
        {
            Console.WriteLine("Yes");

        }
        else
        {
            Console.WriteLine("No");
        }

    }
}