Queue<int> tools = new Queue<int>(Console.ReadLine()
                .Split(" ", StringSplitOptions.RemoveEmptyEntries)
                .Select(int.Parse));

Stack<int> substances = new Stack<int>(Console.ReadLine()
    .Split(" ", StringSplitOptions.RemoveEmptyEntries)
    .Select(int.Parse));

List<int> challenges = Console.ReadLine()
    .Split(" ")
    .Select(int.Parse)
    .ToList();

while (true)
{
    int firstTool = tools.Dequeue();
    int lastSubstance = substances.Pop();

    int result = firstTool * lastSubstance;

    if (challenges.Remove(result))
    { }
    else
    {
        firstTool++;
        tools.Enqueue(firstTool);
        lastSubstance--;
        if (lastSubstance > 0)
        {
            substances.Push(lastSubstance);
        }
    }

    if ((!substances.Any() || !tools.Any()) && challenges.Any())
    {
        Console.WriteLine("Harry is lost in the temple. Oblivion awaits him.");
        break;
    }

    if (!challenges.Any())
    {
        Console.WriteLine("Harry found an ostracon, which is dated to the 6th century BCE.");
        break;
    }
}

if (tools.Any())
{
    Console.WriteLine($"Tools: {string.Join(", ", tools)}");
}

if (substances.Any())
{
    Console.WriteLine($"Substances: {string.Join(", ", substances)}");
}

if (challenges.Any())
{
    Console.WriteLine($"Challenges: {string.Join(", ", challenges)}");
}
