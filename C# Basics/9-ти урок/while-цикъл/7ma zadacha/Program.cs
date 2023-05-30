using System;
internal class Program
{
    private static void Main(string[] args)
    {
        string input = Console.ReadLine();
        double minNumber = double.MaxValue;

        while (input != "Stop")
        {
            double inputAsNumber = double.Parse(input);

            if (inputAsNumber < minNumber)
            {
                minNumber = inputAsNumber;

            }
            input = Console.ReadLine();
        }

        Console.WriteLine(minNumber);

    }
}