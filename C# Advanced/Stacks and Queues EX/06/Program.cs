string[] songs = (Console.ReadLine()
    .Split(", ")
    .ToArray());

Queue<string> queue = new(songs);

while (queue.Count > 0)
{
    string[] command = Console.ReadLine()
        .Split()
        .ToArray();

    switch (command[0])
    {
        case "Play":
            queue.Dequeue();
            break;
        case "Add":
            string addedSong = String.Join(" ", command.Skip(1));
            if (queue.Contains(addedSong))
            {
                Console.WriteLine($"{addedSong} is already contained!");
            }
            else
            {
                queue.Enqueue(addedSong);
            }
            break;
        case "Show":
            Console.WriteLine(String.Join(", ", queue));
            break;
    }
}

Console.WriteLine("No more songs!");
