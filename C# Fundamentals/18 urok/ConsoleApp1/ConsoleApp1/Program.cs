

List<int> wagons = Console.ReadLine()
    .Split()
    .Select(int.Parse)
    .ToList();

int maxCapacity = int.Parse(Console.ReadLine());

string input = Console.ReadLine();

while (input != "end")
{
    string[] command = input.Split(" ");

    if (command.Length == 2)
    {
        int passengers = int.Parse(command[1]);
        wagons.Add(passengers);
    }
    else
    {
        int passengers = int.Parse(command[0]);
        for (int i = 0; i < wagons.Count; i++)
        {
            if (wagons[i] + passengers <= maxCapacity)
            {
                wagons[i] += passengers;
                break;
            }
        }
    }

    input = Console.ReadLine();

}

Console.WriteLine(string.Join(" ", wagons));
