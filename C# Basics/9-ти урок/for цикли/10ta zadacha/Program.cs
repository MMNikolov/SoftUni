using System;

namespace HM4
{
    class Program
    {
        static void Main(string[] args)
        {
            int countCount = int.Parse(Console.ReadLine());
            int sumEven = 0;
            int sumOdd = 0;
            

            for (int i = 0; i < countCount; i++)
            {
                int inputNum = int.Parse(Console.ReadLine());
                if (i % 2 == 0)
                {
                    sumEven += inputNum;
                }
                else
                {
                    sumOdd += inputNum;
                }
            }

            if (sumEven == sumOdd)
            {
                Console.WriteLine("Yes");
                Console.WriteLine($"Sum = {+sumOdd}");

            }
            else
            {
                Console.WriteLine("No");
                Console.WriteLine($"Diff = {Math.Abs(sumEven - sumOdd)}");
            }

        }
    }
}
