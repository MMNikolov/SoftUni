int[] inputNumbers = Console.ReadLine()
    .Split()
    .Select(int.Parse)
    .ToArray();

Queue<int> numbers = new Queue<int>(inputNumbers);

string result = "";

while (numbers.Any())
{
    int number = numbers.Dequeue();

    if (number % 2 == 0)
    {
        result += $"{number}, ";
    }
}

Console.WriteLine(result.TrimEnd(' ', ','));
