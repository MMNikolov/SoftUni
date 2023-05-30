string stops = Console.ReadLine();
string command = Console.ReadLine();

while (command != "Travel")
{
    string[] tokens = command.Split(":");
    string instruction = tokens[0];

    if (instruction == "Add Stop")
    {
        int index = int.Parse(tokens[1]);
        string stop = tokens[2];

        if (index >= 0 && stops.Length >= index)
        {
            stops = stops.Insert(index, stop);
        }
    }
    else if (instruction == "Remove Stop")
    {
        int startIndex = int.Parse(tokens[1]);
        int endIndex = int.Parse(tokens[2]);

        if (startIndex >= 0 && startIndex < stops.Length && endIndex >= 0 && endIndex < stops.Length)
        {
            stops = stops.Remove(startIndex, endIndex - startIndex + 1);
        }
    }
    else if (instruction == "Switch")
    {
        string oldString = tokens[1];
        string newString = tokens[2];

        if (stops.Contains(oldString))
        {
            stops = stops.Replace(oldString, newString);
        }
    }
    Console.WriteLine(stops);
    command = Console.ReadLine();
}

Console.WriteLine($"Ready for world tour! Planned stops: {stops}");
