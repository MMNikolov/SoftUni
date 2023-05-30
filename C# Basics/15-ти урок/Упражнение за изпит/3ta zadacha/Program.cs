using System;
internal class Program
{
    private static void Main(string[] args)
    {
        string destination = Console.ReadLine();
        string dates = Console.ReadLine();
        int number = int.Parse(Console.ReadLine());
        double sum = 0;

        if (destination == "France")
        {
            switch (dates)
            {
                case "21-23":
                    sum = 30;
                    break;
                case "24-27":
                    sum = 35;
                    break;
                case "28-31":
                    sum = 40;
                    break;
                default:
                    break;
            }
        }
        else if (destination == "Italy")
        {
            switch (dates)
            {
                case "21-23":
                    sum = 28;
                    break;
                case "24-27":
                    sum = 32;
                    break;
                case "28-31":
                    sum = 39;
                    break;
                default:
                    break;
            }
        }
        else if (destination == "Germany")
        {
            switch (dates)
            {
                case "21-23":
                    sum = 32;
                    break;
                case "24-27":
                    sum = 37;
                    break;
                case "28-31":
                    sum = 43;
                    break;
                default:
                    break;
            }
        }
        double total = sum * number;
        Console.WriteLine($"Easter trip to {destination} : {total:f2} leva.");

    }
}