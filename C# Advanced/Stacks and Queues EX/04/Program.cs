int allFood = int.Parse(Console.ReadLine());

bool isItTrue = true;

int[] nums = (Console.ReadLine()
    .Split()
    .Select(int.Parse)
    .ToArray());

Queue<int> queue = new(nums);

Console.WriteLine(queue.Max()); 

int allClients = queue.Count();

for (int i = 0; i < allClients; i++)
{
    if (allFood - queue.Peek() >= 0)
    {
        allFood = allFood - queue.Dequeue();
    }
    else
    {
        Console.Write("Orders left: ");
        foreach (var item in queue)
        {
            Console.Write($"{item} ");
            isItTrue = false;
        }
        break;
    }
}

if (isItTrue) 
{
    Console.WriteLine("Orders complete");
}
