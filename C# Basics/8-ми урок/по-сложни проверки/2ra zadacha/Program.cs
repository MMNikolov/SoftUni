using System;
internal class Program
{
    private static void Main(string[] args)
    {
        int temperature = int.Parse(Console.ReadLine());
        string day = Console.ReadLine();
        string outfit = "";
        string shoes = "";

        if (temperature >= 10 && temperature <= 18)
        {
            if (day == "Morning")
            {
                outfit = "Sweatshirt";
                shoes = "Sneakers";
            }
            else if (day == "Afternoon" || day == "Evening")
            {
                outfit = "Shirt";
                shoes = "Moccasins";
            }


        }
        else if (temperature > 18 && temperature <= 24)
        {
            if (day == "Morning" || day == "Evening")
            {
                outfit = "Shirt";
                shoes = "Moccasins";
            }
            else if (day == "Afternoon")
            {
                outfit = "T-Shirt";
                shoes = "Sandals";
            }


        }
        else if (temperature >= 25)
        {
            if (day == "Morning")
            {
                outfit = "T-Shirt";
                shoes = "Sandals";
            }
            else if (day == "Afternoon")
            {
                outfit = "Swim Suit";
                shoes = "Barefoot";
            }
            else if (day == "Evening")
            {
                outfit = "Shirt";
                shoes = "Moccasins";
            }


        }
        Console.WriteLine($"It's {temperature} degrees, get your {outfit} and {shoes}.");

    }
}
