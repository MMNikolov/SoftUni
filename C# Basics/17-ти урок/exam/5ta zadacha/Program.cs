using System;
internal class Program
{
    private static void Main(string[] args)
    {
        int food = int.Parse(Console.ReadLine()) * 1000;
        

        while (true)
        {

            string command = Console.ReadLine();
            if (command == "Adopted")
            {
                break;
            }
            food -= int.Parse(command);
        }
        if (food < 0)
        {
            Console.WriteLine($"Food is not enough. You need {Math.Abs(food)} grams more.");
        }
        else
        {
            Console.WriteLine($"Food is enough! Leftovers: {food} grams.");
        }

    }
}
