using System;
using System.Runtime.ConstrainedExecution;

internal class Program
{
    private static void Main(string[] args)
    {
        string fruits = Console.ReadLine();
        string day = Console.ReadLine();
        double quantity = double.Parse(Console.ReadLine());
        double finalSum = 0;
        bool flag = true;

        if (day == "Monday")
        {
            switch (fruits)
            {
                case "banana": finalSum = quantity * 2.50; break;
                case "apple": finalSum = quantity * 1.20; break;
                case "orange": finalSum = quantity * 0.85; break;
                case "grapefruit": finalSum = quantity * 1.45; break;
                case "kiwi": finalSum = quantity * 2.70; break;
                case "pineapple": finalSum = quantity * 5.50; break;
                case "grapes": finalSum = quantity * 3.85; break;

                default:
                    Console.WriteLine("error");
                    flag = false;
                    break;
            }

        }
        else if (day == "Tuesday")
        {
            switch (fruits)
            {
                case "banana": finalSum = quantity * 2.50; break;
                case "apple": finalSum = quantity * 1.20; break;
                case "orange": finalSum = quantity * 0.85; break;
                case "grapefruit": finalSum = quantity * 1.45; break;
                case "kiwi": finalSum = quantity * 2.70; break;
                case "pineapple": finalSum = quantity * 5.50; break;
                case "grapes": finalSum = quantity * 3.85; break;

                default:
                    Console.WriteLine("error");
                    flag = false;
                    break;
            }
        }
        else if (day == "Wednesday")
        {
            switch (fruits)
            {
                case "banana": finalSum = quantity * 2.50; break;
                case "apple": finalSum = quantity * 1.20; break;
                case "orange": finalSum = quantity * 0.85; break;
                case "grapefruit": finalSum = quantity * 1.45; break;
                case "kiwi": finalSum = quantity * 2.70; break;
                case "pineapple": finalSum = quantity * 5.50; break;
                case "grapes": finalSum = quantity * 3.85; break;

                default:
                    Console.WriteLine("error");
                    flag = false;
                    break;
            }
        }
        else if (day == "Thursday")
        {
            switch (fruits)
            {
                case "banana": finalSum = quantity * 2.50; break;
                case "apple": finalSum = quantity * 1.20; break;
                case "orange": finalSum = quantity * 0.85; break;
                case "grapefruit": finalSum = quantity * 1.45; break;
                case "kiwi": finalSum = quantity * 2.70; break;
                case "pineapple": finalSum = quantity * 5.50; break;
                case "grapes": finalSum = quantity * 3.85; break;

                default:
                    Console.WriteLine("error");
                    flag = false;
                    break;
            }
        }
        else if (day == "Friday")
        {
            switch (fruits)
            {
                case "banana": finalSum = quantity * 2.50; break;
                case "apple": finalSum = quantity * 1.20; break;
                case "orange": finalSum = quantity * 0.85; break;
                case "grapefruit": finalSum = quantity * 1.45; break;
                case "kiwi": finalSum = quantity * 2.70; break;
                case "pineapple": finalSum = quantity * 5.50; break;
                case "grapes": finalSum = quantity * 3.85; break;

                default:
                    Console.WriteLine("error");
                    flag = false;
                    break;
            }

        }
        else if (day == "Saturday")
        {
            switch (fruits)
            {
                case "banana": finalSum = quantity * 2.70; break;
                case "apple": finalSum = quantity * 1.25; break;
                case "orange": finalSum = quantity * 0.90; break;
                case "grapefruit": finalSum = quantity * 1.60; break;
                case "kiwi": finalSum = quantity * 3; break;
                case "pineapple": finalSum = quantity * 5.60; break;
                case "grapes": finalSum = quantity * 4.20; break;

                default:
                    Console.WriteLine("error");
                    flag = false;
                    break;
            }
        }
        else if (day == "Sunday")
        {
            switch (fruits)
            {
                case "banana": finalSum = quantity * 2.70; break;
                case "apple": finalSum = quantity * 1.25; break;
                case "orange": finalSum = quantity * 0.90; break;
                case "grapefruit": finalSum = quantity * 1.60; break;
                case "kiwi": finalSum = quantity * 3; break;
                case "pineapple": finalSum = quantity * 5.60; break;
                case "grapes": finalSum = quantity * 4.20; break;

                default:
                    return;
                    break;
            }

        }
        else
        {
            Console.WriteLine("error");
            flag = false;
        }

        if (flag)
        {
            Console.WriteLine($"{finalSum:f2}");
        }
    }
}