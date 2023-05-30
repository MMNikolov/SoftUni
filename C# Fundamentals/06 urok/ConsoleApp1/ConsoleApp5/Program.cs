using System;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;

internal class Program
{
    private static void Main(string[] args)
    {
        int people = int.Parse(Console.ReadLine());
        string type = Console.ReadLine();
        string day = Console.ReadLine();
        double sum = 0;

        if (type == "Students")
        {
            if (people < 30)
            {
               if (day == "Friday")
               {
                   sum = people * 8.45;
               }
               else if (day == "Saturday")
               {
                   sum = people * 9.80;
               }
               else if (day == "Sunday")
               {
                   sum = people * 10.46;
               }
            }
            else if (people >= 30)
            {
                if (day == "Friday")
                {
                    sum = (people * 8.45) * 0.85;
                }
                else if (day == "Saturday")
                {
                    sum = (people * 9.80) * 0.85;
                }
                else if (day == "Sunday")
                {
                    sum = (people * 10.46) * 0.85;
                }
            }
        }
        else if (type == "Business")
        {
            if (people < 100)
            {

               if (day == "Friday")
               {
                   sum = people * 10.90;
               }
               else if (day == "Saturday")
               {
                   sum = people * 15.60;
               }
               else if (day == "Sunday")
               {
                   sum = people * 16;
               }
            }
            else if (people >= 100)
            {
                if (day == "Friday")
                {
                    sum = people * 10.90 - 10 * 10.90;
                }
                else if (day == "Saturday")
                {
                    sum = people * 15.60 - 10 * 15.60;
                }
                else if (day == "Sunday")
                {
                    sum = people * 16 - 10 * 16;
                }
            }
        }
        else if (type == "Regular")
        {
            if (people < 10 || people > 20)
            {

               if (day == "Friday")
               {
                   sum = people * 15;
               }
               else if (day == "Saturday")
               {
                   sum = people * 20;
               }
               else if (day == "Sunday")
               {
                   sum = people * 22.50;
               }
            }
            else if (people >= 10 && people <= 20)
            {
                if (day == "Friday")
                {
                    sum = (people * 15) * 0.95;
                }
                else if (day == "Saturday")
                {
                    sum = (people * 20) * 0.95;
                }
                else if (day == "Sunday") 
                {
                    sum = (people * 22.50) * 0.95;
                }
            }
        }
        Console.WriteLine($"Total price: {sum:f2}");
    }
}