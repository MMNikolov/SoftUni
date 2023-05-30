using System;
internal class Program
{
    private static void Main(string[] args)
    {
        int numbersCount = int.Parse(Console.ReadLine());
        int maxNumber = int.MinValue;
        int minNumber = int.MaxValue;

        for (int i = 1; i <= numbersCount; i++)
        {
            int currentNumber = int.Parse(Console.ReadLine());

            if (currentNumber < minNumber)
            {
                minNumber = currentNumber;
            }
            if (currentNumber > maxNumber)
            {
                maxNumber = currentNumber;
            }
        }
        Console.WriteLine($"Max number: {maxNumber}");
        Console.WriteLine($"Min number: {minNumber}");

    }
}