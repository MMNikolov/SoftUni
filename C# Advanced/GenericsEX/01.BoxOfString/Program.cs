﻿namespace _01.BoxOfString
{
    internal class Program
    {
        static void Main(string[] args)
        {
            int n = int.Parse(Console.ReadLine());

            for (int i = 0; i < n; i++)
            {
                string text = Console.ReadLine();
                Box<string> box = new Box<string>(text);

                box.ToString();
            }
        }
    }
}