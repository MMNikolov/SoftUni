int[] time = Console.ReadLine()
    .Split(" ", StringSplitOptions.RemoveEmptyEntries)
    .Select(int.Parse)
    .ToArray();

int[] tasks = Console.ReadLine()
    .Split(" ", StringSplitOptions.RemoveEmptyEntries)
    .Select(int.Parse)
    .ToArray();

Queue<int> allTimes = new Queue<int>(time);
Stack<int> allTasks = new Stack<int>(tasks);

int darthVaderDucky = 0;
int thorDucky = 0;
int bigBlueRubberDucky = 0;
int smallYelowRubberDucky = 0;

while (allTimes.Any() && allTasks.Any())
{
    int timeNow = allTimes.Dequeue();
    int taskNow = allTasks.Pop();

    int result = timeNow * taskNow;

    if (result >= 0 && result <= 60)
    {
        darthVaderDucky++;
    }
    else if (result >= 61 && result <= 120)
    {
        thorDucky++;
    }
    else if (result >= 121 && result <= 180)
    {
        bigBlueRubberDucky++;
    }
    else if (result >= 181 && result <= 240)
    {
        smallYelowRubberDucky++;
    }
    else
    {
        taskNow -= 2;

        allTasks.Push(taskNow);
        allTimes.Enqueue(timeNow);
    }
}

Console.WriteLine("Congratulations, all tasks have been completed! Rubber ducks rewarded:");
Console.WriteLine($"Darth Vader Ducky: {darthVaderDucky}");
Console.WriteLine($"Thor Ducky: {thorDucky}");
Console.WriteLine($"Big Blue Rubber Ducky: {bigBlueRubberDucky}");
Console.WriteLine($"Small Yellow Rubber Ducky: {smallYelowRubberDucky}");