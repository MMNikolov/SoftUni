using System;
internal class Program
{
    private static void Main(string[] args)
    {
        int n = int.Parse(Console.ReadLine());

        for (int i = 0; i < n; i++)
        {
            int currentNumber = i;

            int sum = 0;

            while (true)
            {
                sum += currentNumber % 10;

                currentNumber /= 10;
            }

            if (sum == 5 || sum == 7 || sum == 11)
            {
                Console.WriteLine($"{i} -> True");
            }
            else
            {
                Console.WriteLine($"{i} -> False");
            }
        }


    }
}