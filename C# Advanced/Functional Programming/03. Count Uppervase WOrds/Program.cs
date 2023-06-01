string[] words = Console.ReadLine()
    .Split(' ', StringSplitOptions.RemoveEmptyEntries);

Func<string, bool> isUpper = w => Char.IsUpper(w[0]);

string[] upperCaseWords = 
    words.Where(isUpper)
    .ToArray();

foreach (var word in upperCaseWords)
{
    Console.WriteLine(word);
}
