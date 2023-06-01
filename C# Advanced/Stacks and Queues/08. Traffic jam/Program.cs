int n = int.Parse(Console.ReadLine());

Queue<string> trafficJam = new Queue<string>();

string command = Console.ReadLine();
int carsCount = 0;  

while (command != "end")
{
    if (command != "green")
    {
        trafficJam.Enqueue(command);
        command = Console.ReadLine();
        continue;
    }

    int currentCount = n;

    while (trafficJam.Count > 0 && currentCount > 0)
    {
        Console.WriteLine($"{trafficJam.Dequeue()} passed!");
        currentCount--;

        carsCount++;
    }
    
    
    command = Console.ReadLine();
}

Console.WriteLine($"{carsCount} cars passed the crossroads.");
