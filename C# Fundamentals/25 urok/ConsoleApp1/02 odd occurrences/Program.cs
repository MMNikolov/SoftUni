string[] words = Console.ReadLine().Split();

Dictionary<string, int> count = new Dictionary<string, int>();

foreach (var word in words)
{
    string lowerCaseOfWord = word.ToLower();
    if (count.ContainsKey(lowerCaseOfWord))
    {
        count[lowerCaseOfWord]++;
    }
    else
    {
        count.Add(lowerCaseOfWord, 1);
    }
}

foreach (var lowerCaseOfWord in count)
{
    if (lowerCaseOfWord.Value % 2 != 0)
    {
        Console.Write($"{lowerCaseOfWord.Key} ");
    }
}
