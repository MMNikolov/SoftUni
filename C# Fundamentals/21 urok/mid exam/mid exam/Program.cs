using System;
using System.Collections.Generic;
using System.Linq;

namespace _03._AngryCat
{
    internal class Program
    {
        static void Main(string[] args)
        {
            List<int> ratings = Console.ReadLine().Split(", ", StringSplitOptions.RemoveEmptyEntries).Select(int.Parse).ToList();
            int entryPoint = int.Parse(Console.ReadLine());
            string typeOfItem = Console.ReadLine();
            int rightSum = 0;
            int leftSum = 0;

            //if (ratings.Count == 1 || ratings.Count == 2)
            //{
            //   return; 
            //}
            if (typeOfItem == "cheap")
            {
                for (int i = entryPoint + 1; i < ratings.Count; i++)
                {
                    if (ratings[i] < ratings[entryPoint])
                    {
                        rightSum += ratings[i];
                    }
                    else
                    {
                        continue;
                    }
                }

                for (int i = entryPoint - 1; i >= 0; i--)
                {
                    if (ratings[i] < ratings[entryPoint])
                    {
                        leftSum += ratings[i];
                    }
                    else
                    {
                        continue;
                    }
                }
            }
            else if (typeOfItem == "expensive")
            {
                for (int i = entryPoint + 1; i < ratings.Count; i++)
                {
                    if (ratings[i] >= ratings[entryPoint])
                    {
                        rightSum += ratings[i];
                    }
                    else
                    {
                        continue;
                    }
                }

                for (int i = entryPoint - 1; i >= 0; i--)
                {
                    if (ratings[i] >= ratings[entryPoint])
                    {
                        leftSum += ratings[i];
                    }
                    else
                    {
                        continue;
                    }
                }
            }
            else
            {
                return;
            }

            if (rightSum > leftSum)
            {
                Console.WriteLine($"Right - {rightSum}");
            }
            else if (leftSum > rightSum)
            {
                Console.WriteLine($"Left - {leftSum}");
            }
            else if (rightSum == leftSum)
            {
                Console.WriteLine($"Left - {leftSum}");
            }
        }
    }
}

