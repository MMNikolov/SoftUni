Queue<int> textiles = new Queue<int>(Console.ReadLine()
    .Split(" ", StringSplitOptions.RemoveEmptyEntries)
    .Select(int.Parse));

Stack<int> medicaments = new Stack<int>(Console.ReadLine()
    .Split(" ", StringSplitOptions.RemoveEmptyEntries)
    .Select(int.Parse));

Dictionary<int, string> table = new Dictionary<int, string>()
{
    {30, "Patch"},
    {40, "Bandage"},
    {100, "MedKit"}
};

Dictionary<string, int> items = new Dictionary<string, int>();

while (textiles.Any() && medicaments.Any())
{
    int result = textiles.Peek() + medicaments.Peek();

    if (table.Any(x => x.Key == result))
    {
        if (!items.Any(x => x.Key == table[result]))
        {
            items.Add(table[result], 0);
        }

        items[table[result]]++;

        textiles.Dequeue();
        medicaments.Pop();
    }
    else
    {
        if (result > 100)
        {
            if (!items.Any(x => x.Key == "MedKit"))
            {
                items.Add("MedKit", 0);
            }

            items["MedKit"]++;

            result -= 100;
            medicaments.Pop();
            medicaments.Push(medicaments.Pop() + result);
            textiles.Dequeue();
        }
        else
        {
            medicaments.Push(medicaments.Pop() + 10);
            textiles.Dequeue();
        }
    }
}

if (!textiles.Any() && !medicaments.Any())
{
    Console.WriteLine("Textiles and medicaments are both empty.");
}
else if (!textiles.Any())
{
    Console.WriteLine("Textiles are empty.");
}
else
{
    Console.WriteLine("Medicaments are empty.");
}

foreach (var item in items.OrderByDescending(x => x.Value).ThenBy(y => y.Key))
{
    Console.WriteLine($"{item.Key} - {item.Value}");
}

if (textiles.Any())
{
    Console.WriteLine($"Textiles left: {string.Join(", ", textiles)}");
}

if (medicaments.Any())
{
    Console.WriteLine($"Medicaments left: {string.Join(", ", medicaments)}");
}