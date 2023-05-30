using System;
internal class Program
{
    private static void Main(string[] args)
    {
        int N = int.Parse(Console.ReadLine());
        int M = int.Parse(Console.ReadLine()); 
        int S = int.Parse(Console.ReadLine());

        for (int i = M; i >= N; i--)
        {
            
            if (i != S)
            {
                if (i % 6 == 0)
                {
                    Console.Write($"{i} ");
                }
            }
            else if (i == S && i % 6 == 0)
            {
                break;
            }
        }

    }
}
