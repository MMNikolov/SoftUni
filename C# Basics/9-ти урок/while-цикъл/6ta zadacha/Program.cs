using System;
internal class Program
{
    private static void Main(string[] args)
    {
        string input = Console.ReadLine();
        double maxNumber = double.MinValue;

        while (input != "Stop")
        {
            double inputAsNumber = double.Parse(input);

            if (inputAsNumber > maxNumber)
            {
                maxNumber = inputAsNumber;

            }
            input = Console.ReadLine();
        }

        Console.WriteLine(maxNumber);

    }
}