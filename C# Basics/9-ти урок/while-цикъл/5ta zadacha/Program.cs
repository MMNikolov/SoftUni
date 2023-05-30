using System;
internal class Program
{
    private static void Main(string[] args)
    {
        string input = Console.ReadLine();
        double totalSum = 0;

        while (input != "NoMoreMoney")
        {
            double inputAsNumber = double.Parse(input);
            if (inputAsNumber < 0 )
            {
                Console.WriteLine("Invalid operation!");
                break;
            }
            Console.WriteLine($"Increase: {inputAsNumber:f2}");

            totalSum += inputAsNumber;

            input = Console.ReadLine();

        }

        Console.WriteLine($"Total: {totalSum:f2}");

    }
}