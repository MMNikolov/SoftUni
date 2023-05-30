int[] inputNumbers = Console.ReadLine()
    .Split()
    .Select(int.Parse)
    .ToArray();

int odd = 0;
int even = 0;

for (int i = 0; i < inputNumbers.Length; i++)
{
    if (inputNumbers[i] % 2 == 0)
    {
        even += inputNumbers[i];
    }
    else if (inputNumbers[i] % 2 != 0)
    {
        odd += inputNumbers[i];
    }
}

int sum = even - odd;

Console.WriteLine(sum);
