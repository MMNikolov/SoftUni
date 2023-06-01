string[] inputNames = Console.ReadLine().Split();
int n = int.Parse(Console.ReadLine());

Queue<string> people = new Queue<string>(inputNames);

while (people.Count > 1)
{
	for (int i = 0; i < n - 1; i++)
	{
		people.Enqueue(people.Dequeue());
	}

	Console.WriteLine($"Removed {people.Dequeue()}");
}

Console.WriteLine($"Last is {people.Peek()}");
