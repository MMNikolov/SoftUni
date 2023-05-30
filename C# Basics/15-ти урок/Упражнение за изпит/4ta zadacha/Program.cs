using System;
internal class Program
{
    private static void Main(string[] args)
    {
        int howManyEggs = int.Parse(Console.ReadLine());
        int orange = 0;
        int blue = 0;
        int green = 0;
        int red = 0;

        string colourOfMaxEggs = "";
        int maxEggs = int.MinValue;

        for (int i = 0; i < howManyEggs; i++)
        {
            string colour = Console.ReadLine();

            switch (colour)
            {
                case "red":
                    red++;
                    if (red > maxEggs)
                    {
                        maxEggs = red;
                        colourOfMaxEggs = "red";
                    }
                        break;
                case "blue":
                    blue++;
                    if (blue > maxEggs)
                    {
                        maxEggs = blue;
                        colourOfMaxEggs = "blue";
                    }
                    break;
                case "green":
                    green++;
                    if (green > maxEggs)
                    {
                        maxEggs = green;
                        colourOfMaxEggs = "green";
                    }
                    break;
                case "orange":
                    orange++;
                    if (orange > maxEggs)
                    {
                        maxEggs = orange;
                        colourOfMaxEggs = "orange";
                    }
                    break;
                default:
                    break;
            }
        }
        Console.WriteLine($"Red eggs: {red}");
        Console.WriteLine($"Orange eggs: {orange}");
        Console.WriteLine($"Blue eggs: {blue}");
        Console.WriteLine($"Green eggs: {green}");
        Console.WriteLine($"Max eggs: {maxEggs} -> {colourOfMaxEggs}");
    }
}