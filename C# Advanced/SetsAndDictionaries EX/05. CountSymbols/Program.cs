SortedDictionary<char, int> charCounts = new();

string input = Console.ReadLine();

foreach (var ch in input)
{
	if (!charCounts.ContainsKey(ch))
	{
		charCounts.Add(ch, 0);
	}

	charCounts[ch]++;
}

foreach (var charCount in charCounts)
{
	Console.WriteLine($"{charCount.Key}: {charCount.Value} time/s");
}
