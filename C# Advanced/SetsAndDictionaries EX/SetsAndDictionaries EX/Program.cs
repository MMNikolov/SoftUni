int n = int.Parse(Console.ReadLine());

HashSet<string> strings = new();

for (int i = 0; i < n; i++)
{
    string name = Console.ReadLine();

	if (!strings.Contains(name))
	{
		strings.Add(name);
	}
}

foreach (var name in strings)
{
	Console.WriteLine(name);
}
