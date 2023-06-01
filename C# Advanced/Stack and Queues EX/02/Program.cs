using System.Runtime.Serialization.Formatters;

int[] tokens = Console.ReadLine()
    .Split(" ", StringSplitOptions.RemoveEmptyEntries)
    .Select(int.Parse)
    .ToArray();

int[] numbers = Console.ReadLine()
    .Split(" ", StringSplitOptions.RemoveEmptyEntries)
    .Select(int.Parse)
    .ToArray();

int elementsToQueue = tokens[0];
int elementsToDequeue = tokens[1];
int number = tokens[2];

Queue<int> queue = new();

for (int i = 0; i < elementsToQueue; i++)
{
    queue.Enqueue(numbers[i]);
}

for (int i = 0; i < elementsToDequeue; i++)
{
    queue.Dequeue();
}

if (queue.Contains(number))
{
    Console.WriteLine("true");
}
else if (!queue.Contains(number))
{
    if (queue.Any())
    {
        Console.WriteLine(queue.Min());
    }
    else
    {
        Console.WriteLine("0");
    }
}





