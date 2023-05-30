List<string> elements = Console.ReadLine()
    .Split(' ', StringSplitOptions.RemoveEmptyEntries)
    .ToList(); //1 1 2 2 3 3 4 4 5 5 

string command = Console.ReadLine(); //"1 0", "-1 0"

int moves = 0;

while (command != "end")
{
    int[] indexes = command
        .Split(' ', StringSplitOptions.RemoveEmptyEntries) //["1", "0"]
        .Select(int.Parse) //[1, 0]
        .ToArray();

    int firstIndex = indexes[0]; //1
    int secondIndex = indexes[1]; //0

    moves++;

    if (firstIndex == secondIndex
        || firstIndex < 0
        || firstIndex >= elements.Count
        || secondIndex < 0
        || secondIndex >= elements.Count)
    {
        //cheat -na
        int middleIndex = elements.Count / 2;

        elements.Insert(middleIndex, $"-{moves}a");
        elements.Insert(middleIndex, $"-{moves}a");

        Console.WriteLine("Invalid input! Adding additional elements to the board");
    }
    else if (elements[firstIndex] == elements[secondIndex])
    {
        Console.WriteLine($"Congrats! You have found matching elements - {elements[firstIndex]}!");

        if (firstIndex > secondIndex)
        {
            elements.RemoveAt(firstIndex);
            elements.RemoveAt(secondIndex);
        }
        else
        {
            elements.RemoveAt(secondIndex);
            elements.RemoveAt(firstIndex);
        }
    }
    else if (elements[firstIndex] != elements[secondIndex])
    {
        Console.WriteLine("Try again!");
    }

    if (elements.Count == 0)
    {
        Console.WriteLine($"You have won in {moves} turns!");
        break;
    }

    command = Console.ReadLine();
}

if (elements.Count > 0)
{
    Console.WriteLine("Sorry you lose :(");
    Console.WriteLine(string.Join(" ", elements));
}
