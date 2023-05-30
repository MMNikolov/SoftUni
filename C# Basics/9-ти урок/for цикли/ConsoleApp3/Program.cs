using System;
internal class Program
{
    private static void Main(string[] args)
    {
        int highest = int.Parse(Console.ReadLine());

        for (int i = highest; i >= 1; --i)
        {
            Console.WriteLine(i);
        }

    }
}