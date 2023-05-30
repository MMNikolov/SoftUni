using System;
internal class Program
{
    private static void Main(string[] args)
    {
        string productName = Console.ReadLine();
        string town = Console.ReadLine();
        double quantity = double.Parse(Console.ReadLine());
        double finalSum = 0;

        if (town == "Sofia")
        {
            switch (productName)
            {
                case "coffee": finalSum = quantity * 0.50;break;
                case "water": finalSum = quantity * 0.80; break;
                case "beer": finalSum = quantity * 1.20; break;
                case "sweets": finalSum = quantity * 1.45; break;
                case "peanuts": finalSum = quantity * 1.60; break;
                default:
                    break;
            }
        }
        else if (town == "Plovdiv")
        {
            switch (productName)
            {
                case "coffee": finalSum = quantity * 0.40; break;
                case "water": finalSum = quantity * 0.70; break;
                case "beer": finalSum = quantity * 1.15; break;
                case "sweets": finalSum = quantity * 1.30; break;
                case "peanuts": finalSum = quantity * 1.50; break;
                default:
                    break;
            }
        }
        else if (town == "Varna")
        {
            switch (productName)
            {
                case "coffee": finalSum = quantity * 0.45; break;
                case "water": finalSum = quantity * 0.70; break;
                case "beer": finalSum = quantity * 1.10; break;
                case "sweets": finalSum = quantity * 1.35; break;
                case "peanuts": finalSum = quantity * 1.55; break;
                default:
                    break;
            }
        }
        Console.WriteLine(finalSum);
    }
}