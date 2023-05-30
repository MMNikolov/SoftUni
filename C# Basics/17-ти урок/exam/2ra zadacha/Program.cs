using System;
internal class Program
{
    private static void Main(string[] args)
    {
        string name = Console.ReadLine();
        double budget = double.Parse(Console.ReadLine());
        int beer = int.Parse(Console.ReadLine());   
        int chips = int.Parse(Console.ReadLine());

        double beerPrice = beer * 1.2;
        double chipsPrice = beerPrice * 0.45;
        double allChips = chipsPrice * chips;
        double chipsHigher = Math.Ceiling(allChips);
        double sum = chipsHigher + beerPrice;

        if (budget >= sum)
        {
            Console.WriteLine($"{name} bought a snack and has {budget - sum:f2} leva left.");
        }
        else if (sum > budget)
        {
            Console.WriteLine($"{name} needs {sum - budget:f2} more leva!");
        }

    }
}
