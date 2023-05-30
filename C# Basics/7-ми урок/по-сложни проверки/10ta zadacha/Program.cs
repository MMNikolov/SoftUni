using System;
internal class Program
{
    private static void Main(string[] args)
    {
        int number = int.Parse(Console.ReadLine());

        if (number >= 100 && number <= 200 || number == 0)
        {

        }
        else if (number < 100 || number > 200)
        {
            Console.WriteLine("invalid");
        }

    }
}