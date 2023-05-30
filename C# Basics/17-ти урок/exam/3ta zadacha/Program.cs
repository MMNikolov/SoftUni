using System;
internal class Program
{
    private static void Main(string[] args)
    {
        string sweets = Console.ReadLine();
        int amountSweets = int.Parse(Console.ReadLine());
        int dayDecember = int.Parse(Console.ReadLine());
        double money = 0;

        if (sweets == "Cake")
        {
            if (dayDecember <= 22)
            {
            if (dayDecember <= 15)
            {
                money = (24 * amountSweets) * 0.90;

                if (money >= 100 && money <= 200)
                {
                    Console.WriteLine($"{money * 0.85:f2}");
                }
                else if (money > 200)
                {
                    Console.WriteLine($"{money * 0.75:f2}");
                }
            }
            else if (dayDecember > 15)
            {
                money = 28.70 * amountSweets;

                if (money >= 100 && money <= 200)
                {
                    Console.WriteLine($"{money * 0.85:f2}");
                }
                else if (money > 200)
                {
                    Console.WriteLine($"{money * 0.75:f2}");
                }
            }
            }

        }
        else if (sweets == "Souffle")
        {
            if (dayDecember <= 22)
            {

            if (dayDecember <= 15)
            {
                money = (6.66 * amountSweets) * 0.90;

                if (money >= 100 && money <= 200)
                {
                    Console.WriteLine($"{money * 0.85:f2}");
                }
                else if (money > 200)
                {
                    Console.WriteLine($"{money * 0.75:f2}");
                }
            }
            else if (dayDecember > 15)
            {
                money = 9.80 * amountSweets;

                if (money >= 100 && money <= 200)
                {
                    Console.WriteLine($"{money * 0.85:f2}");
                }
                else if (money > 200)
                {
                    Console.WriteLine($"{money * 0.75:f2}");
                }
            }
            }
        }
        else if (sweets == "Baklava")
        {
            if (dayDecember <= 22)
            {

            if (dayDecember <= 15)
            {
                money = (12.60 * amountSweets) * 0.90;

                if (money >= 100 && money <= 200)
                {
                    Console.WriteLine($"{money * 0.85:f2}");
                }
                else if (money > 200)
                {
                    Console.WriteLine($"{money * 0.75:f2}");
                }
            }
            else if (dayDecember > 15)
            {
                money = 16.98 * amountSweets;

                if (money >= 100 && money <= 200)
                {
                    Console.WriteLine($"{money * 0.85:f2}");
                }
                else if (money > 200)
                {
                    Console.WriteLine($"{money * 0.75:f2}");
                }
            }
            }
        }

    }
}
