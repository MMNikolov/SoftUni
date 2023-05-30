List<int> numbers = Console.ReadLine()
    .Split()
    .Select(int.Parse)
    .ToList();

List<int> result = new List<int>();


if (numbers.Count > 0)
{
    numbers.Reverse();
    Console.WriteLine(string.Join(" ", result));
}
else
{
    Console.WriteLine("empty");
}


