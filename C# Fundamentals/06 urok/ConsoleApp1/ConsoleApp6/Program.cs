using System;
internal class Program
{
    private static void Main(string[] args)
    {
        int first = int.Parse(Console.ReadLine());
        int second = int.Parse(Console.ReadLine());
        double sum = 0;

        for (int i = first; i < second; i++)
        {
            Console.Write(i);
        }
        Console.WriteLine($"Sum: {sum}");
    }
}