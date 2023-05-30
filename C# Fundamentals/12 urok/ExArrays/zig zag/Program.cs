﻿int n = int.Parse(Console.ReadLine());

int[] firstArray = new int[n];
int[] secondArray = new int[n];

for (int i = 0; i < n; i++)
{
    int[] currentInput = Console.ReadLine()
        .Split()
        .Select(int.Parse)
        .ToArray();

    if (i % 2 == 0)
    {
        firstArray[i] = currentInput[0];
        secondArray[i] = currentInput[1];
    }
    else
    {
        firstArray[i] = currentInput[1];
        secondArray[i] = currentInput[0];
    }
}

Console.WriteLine(string.Join(" ", firstArray));
Console.WriteLine(string.Join(" ", secondArray));
