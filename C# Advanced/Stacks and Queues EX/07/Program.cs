int n = int.Parse(Console.ReadLine());
var petrol = new Queue<int>();
var distance = new Queue<int>();
int[] input;

for (int i = 0; i < n; i++)
{
    input = Console.ReadLine()
        .Split(' ')
        .Select(int.Parse)
        .ToArray();

    petrol.Enqueue(input[0]);
    distance.Enqueue(input[1]);
}

int fuelNow;
var petrol2 = new Queue<int>();
var distance2 = new Queue<int>();

for (int i = 0; i < n; i++)
{
    fuelNow = petrol.Peek();
    for (int x = 0; x < n; x++)
    {
        if (distance.Peek() <= fuelNow)
        {
            fuelNow -= distance.Peek();
            if (x == n - 1)
            {
                Console.WriteLine(i);
                return;
            }
        }
        else
        {
            for (int y = x; y < n; y++)
            {
                petrol.Enqueue(petrol.Dequeue());
                distance.Enqueue(distance.Dequeue());
            }
            break;
        }
        petrol.Enqueue(petrol.Dequeue());
        distance.Enqueue(distance.Dequeue());
        fuelNow += petrol.Peek();
    }
    petrol.Enqueue(petrol.Dequeue());
    distance.Enqueue(distance.Dequeue());
}