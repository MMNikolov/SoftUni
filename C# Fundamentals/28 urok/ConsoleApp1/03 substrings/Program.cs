string wordsToRemove = Console.ReadLine();
string word = Console.ReadLine();

while (word.Contains(wordsToRemove))
{
    int index = word.IndexOf(wordsToRemove);
    word = word.Remove(index, wordsToRemove.Length);
}

Console.WriteLine(word);