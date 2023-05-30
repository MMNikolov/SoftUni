using System;

int[] first = Console.ReadLine()
    .Split()
    .Select(int.Parse)
    .ToArray();


int[] second = Console.ReadLine()
    .Split()
    .Select(int.Parse)
    .ToArray();

bool flag = true;

for (int i = 0; i < first.Length; i++)
{
    if (first[i] != second[i])
    {
        Console.WriteLine($"Arrays are not identical. Found difference at {i} index");
        return;
    }
    

}
Console.WriteLine($"Arrays are identical. Sum: {first.Sum()}");

