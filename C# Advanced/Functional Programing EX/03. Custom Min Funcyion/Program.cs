Func<int[], int> smallestNumber =
                number => number.Min();

int[] numbers = Console.ReadLine()
    .Split(" ", StringSplitOptions.RemoveEmptyEntries)
    .Select(int.Parse)
    .ToArray();

Console.WriteLine(smallestNumber(numbers));
