string[] words = Console.ReadLine().Split();

string result = string.Empty;

foreach (string word in words)
{
    int length = word.Length;

    for (int i = 0; i < length; i++)
    {
        result += word;
    }
}

Console.WriteLine(result);
