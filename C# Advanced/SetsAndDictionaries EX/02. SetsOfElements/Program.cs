int[] ints = Console.ReadLine()
    .Split(" ", StringSplitOptions.RemoveEmptyEntries)
    .Select(int.Parse)
    .ToArray();


HashSet<int> secondSet = new HashSet<int>();
HashSet<int> firstSet = new();

for (int i = 0; i < ints[0]; i++)
{
    firstSet.Add(int.Parse(Console.ReadLine()));
}

for (int i = 0; i < ints[1]; i++)
{
    secondSet.Add(int.Parse(Console.ReadLine()));
}

firstSet.IntersectWith(secondSet);

Console.WriteLine(string.Join(" ", firstSet));


