int[] numbers = Console.ReadLine()
    .Split()
    .Select(int.Parse)
    .OrderByDescending(n => n)
    .ToArray();

int count = numbers.Length >= 3 ? 3 : numbers.Length;

for (int i = 0; i < count; i++)
{
    Console.Write($"{numbers[i]} ");
}
