using System;
internal class Program
{
    private static void Main(string[] args)
    {
        int location = int.Parse(Console.ReadLine());
        int totalGold = 0;

        for (int i = 0; i < location; i++)
        {
            double expectedGold = double.Parse(Console.ReadLine());
            int days = int.Parse(Console.ReadLine());
            double amountOfGold = 0;

            for (int j = 0; j < days; j++)
            {
                amountOfGold += double.Parse(Console.ReadLine());
            }
            double kg = amountOfGold / days;

            if (kg >= expectedGold)
            {
                Console.WriteLine($"Good job! Average gold per day: {kg:f2}.");
            }
            else if (kg < expectedGold)
            {
                Console.WriteLine($"You need {expectedGold - kg:f2} gold.");
            }
        }

    }
}
