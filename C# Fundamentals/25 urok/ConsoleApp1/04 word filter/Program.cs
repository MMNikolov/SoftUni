string[] words =
                Console.ReadLine()
                .Split()
                .Where(w => w.Length % 2 == 0)
                .ToArray();

foreach (var word in words)
{
    Console.WriteLine(word);
}
