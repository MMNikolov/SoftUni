using System;
internal class Program
{
    private static void Main(string[] args)
    {
        string flowers = Console.ReadLine();
        int count = int.Parse(Console.ReadLine());
        int budget = int.Parse(Console.ReadLine());
        double totalSum = 0;
        double roseSum = count * 5;
        double dahliasSum = count * 3.80;
        double tulipSum = count * 2.80;
        double narcissusSum = count * 3;
        double gladiolusSum = count * 2.50;

        if (flowers == "Roses")
        {
            if (count > 80)
            {
                totalSum = roseSum * 0.90;
            }
            else
            {
                totalSum = roseSum;
            }
        }
        else if (flowers == "Dahlias")
        {
            if (count > 90)
            {
                totalSum = dahliasSum * 0.85;
            }
            else
            {
                totalSum = dahliasSum;
            }
        }
        else if (flowers == "Tulips")
        {
            if (count > 80)
            {
                totalSum = tulipSum * 0.85;
            }
            else
            {
                totalSum = tulipSum;
            }
        }
        else if (flowers == "Narcissus")
        {
            if (count > 120)
            {
                totalSum = narcissusSum * 0.85;
            }
            else
            {
                totalSum = narcissusSum;
            }
        }
        else if (flowers == "Gladiolus")
        {
            if (count > 80)
            {
                totalSum = gladiolusSum * 0.80;
            }
            else
            {
                totalSum = gladiolusSum;
            }
        }

        if (budget > totalSum)
        {
            Console.WriteLine($"Hey, you have a great garden with {count} {flowers} and {budget - totalSum:f2} leva left.");
        }
        else if (totalSum > budget)
        {
            Console.WriteLine($"Not enough money, you need {totalSum - budget:f2} leva more.");
        }

    }
}
