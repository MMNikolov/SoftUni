using System.Collections;

Queue<int> tools = new Queue<int>(Console.ReadLine()
    .Split(" ", StringSplitOptions.RemoveEmptyEntries)
    .Select(int.Parse));

Stack<int> substances = new Stack<int>(Console.ReadLine()
    .Split(" ", StringSplitOptions.RemoveEmptyEntries)
    .Select(int.Parse));

int[] challenges = Console.ReadLine()
    .Split(" ")
    .Select(int.Parse)
    .ToArray();

bool flag = false;

//Dictionary<string, int> stuff = new Dictionary<string, int>()
//{
//    { "Tools", tools.Count },
//    { "Substances", substances.Count },
//    { "Challenges", challenges.Length }
//};

while (tools.Any() && substances.Any() && challenges.Any())
{
    int currentTool = tools.Dequeue();
    int currentSubstance = substances.Pop();

    int result = currentTool * currentSubstance;

    foreach (var item in challenges)
    {
        if (result == item)
        {
            challenges = challenges.Where(c => c != result).ToArray();
            flag = true;
        }

    }
    if (!flag)
    {
        currentTool += 1;
        tools.Enqueue(currentTool);
        currentSubstance -= 1;
        if (currentSubstance != 0)
        {
            substances.Push(currentSubstance);
        }
    }
}

if ((!tools.Any() || !substances.Any()) && challenges.Any())
{
    Console.WriteLine("Harry is lost in the temple. Oblivion awaits him.");
}
else if (!challenges.Any())
{
    Console.WriteLine("Harry found an ostracon, which is dated to the 6th century BCE."); 
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

