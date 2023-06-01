Action<string[], string> print = (names, title) =>
{
    foreach (var name in names)
    {
        Console.WriteLine($"{title} {name}");
    }
};

string[] names = Console.ReadLine()
    .Split(" ", StringSplitOptions.RemoveEmptyEntries);

print(names, "Sir");
