using System;
internal class Program
{
    private static void Main(string[] args)
    {
        int goalNumber = int.Parse(Console.ReadLine());
        int sumOfNumbers = 0;

        while (sumOfNumbers < goalNumber)
        {
            int currentNumber = int.Parse(Console.ReadLine());

            sumOfNumbers += currentNumber;
        }

        Console.WriteLine(sumOfNumbers);

    }
}