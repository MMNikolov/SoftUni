Dictionary<string, List<string>> WordAndSynonyms = new Dictionary<string, List<string>>();
int input = int.Parse(Console.ReadLine());

for (int i = 0; i < input; i++)
{
    string word = Console.ReadLine();
    string synonym = Console.ReadLine();

    if (WordAndSynonyms.ContainsKey(word) == false)
    {
        WordAndSynonyms.Add(word, new List<string>());
        WordAndSynonyms[word].Add(synonym);
    }
    else if (WordAndSynonyms.ContainsKey(word))
    {
        WordAndSynonyms[word].Add(synonym);
    }
}

foreach (var word in WordAndSynonyms)
{
    Console.WriteLine($"{word.Key} - {string.Join(", ", word.Value)}");
}
