using System;
internal class Program
{
    private static void Main(string[] args)
    {
        int number = int.Parse(Console.ReadLine());

        for (int i = 1; i <= number; i += 3)
        {
            Console.WriteLine(i);
        }

    }
}