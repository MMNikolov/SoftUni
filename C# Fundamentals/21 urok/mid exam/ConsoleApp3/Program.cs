List<int> list = Console.ReadLine()
    .Split(" ")
    .Select(int.Parse)
    .ToList();

bool flag = true;

while (true)
{
    string input = Console.ReadLine();
    if (input == "Finish")
    {
        break;
    }

    string[] tokens = input.Split(" ", StringSplitOptions.RemoveEmptyEntries);

    string command = tokens[0];
    int index = int.Parse(tokens[1]);

    if (tokens[0] == "Add")
    {
        list.Add(index);
    }
    else if (tokens[0] == "Remove")
    {
        for (int i = 0; i < list.Count; i++)
        {
            if (list[i] == index)
            {
                list.RemoveAt(i);
                break;
            }

        }
    }
    else if (tokens[0] == "Replace")
    {
        int value = int.Parse(tokens[2]);

        for (int i = 0; i < list.Count; i++)
        {
            if (list[i] == index)
            {
                list[i] = value;
                break;
            }
        }
    }
    else if (tokens[0] == "Collapse")
    {
        List<int> copy = list.ToList();
        foreach (var number in copy)
        {
            if (number < index)
            {
                list.Remove(number);
            }
        }
        break;
    }

}

Console.WriteLine(string.Join(" ", list));
