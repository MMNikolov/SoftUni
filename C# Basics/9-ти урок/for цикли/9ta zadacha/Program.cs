using System;

namespace HM4
{
    class Program
    {
        static void Main(string[] args)
        {
            int n = int.Parse(Console.ReadLine());
            int sum1 = 0;
            int sum2 = 0;

            for (int i = 0; i < n; i++)
            {
                int num = int.Parse(Console.ReadLine());

                sum1 += num;
            }

            for (int i = 0; i < n; i++)
            {
                int num = int.Parse(Console.ReadLine());

                sum2 += num;
            }

            if (sum1 == sum2)
            {
                Console.WriteLine($"Yes, sum = {sum1}");
            }
            else if (sum1 > sum2)
            {
                int diff = sum1 - sum2;

                Console.WriteLine($"No, diff = {diff}");
            }
            else if (sum2 > sum1)
            {
                int diff = sum2 - sum1;
                Console.WriteLine($"No, diff = {diff}");
            }

        }
    }
}