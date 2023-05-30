List<string> items = Console.ReadLine()
    .Split('|')
    .ToList();

string command = Console.ReadLine();

while (command != "Yohoho!")
{
    string[] commandInfo = command.Split();

    if (commandInfo[0] == "Loot")
    {
        foreach (var item in commandInfo.Skip(1))
        {
            if (!items.Contains(item))
            {
                items.Insert(0, item);
            }
        }
    }
    else if (commandInfo[0] == "Drop")
    {
        int index = int.Parse(commandInfo[1]);

        if (index >= 0 && index < items.Count)
        {
            string currentElements = items[index];
            items.RemoveAt(index);
            items.Add(currentElements);
        }
    }
    else if (commandInfo[0] == "Steal")
    {
        List<string> elements = new List<string>();

        int count = int.Parse(commandInfo[1]);

        while (count > 0 && items.Count > 0)
        {
            string currentElement = items[items.Count - 1];
            elements.Add(currentElement);

            items.RemoveAt(items.Count - 1);

            count--;
        }

        elements.Reverse();

        Console.WriteLine(string.Join(", ", elements));
    }


}


if (items.Count == 0)
{
    Console.WriteLine("Failed treasure hunt.");
}
else
{

    int sum = 0;

    foreach (string item in items)
    {
        sum += item.Length;
    }

    double result = sum/ (double)items.Count;

    Console.WriteLine($"Average treasure gain: {result:F2} pirate credits.");
}
