int initialEnergy = int.Parse(Console.ReadLine());

string command = Console.ReadLine();
int battlesWon = 0;
bool hasEnoughtEnergy = true;

while (command != "End of battle")
{
    int distance = int.Parse(command);

    if (initialEnergy - distance < 0)
    {
        hasEnoughtEnergy = false;
        break;
    }

    initialEnergy -= distance;
    battlesWon++;

    if (battlesWon % 3 == 0)
    {
        initialEnergy += battlesWon;
    }

    command = Console.ReadLine();
}

if (hasEnoughtEnergy)
{
    Console.WriteLine($"Won battles: {battlesWon}. Energy left: {initialEnergy}");
}
else
{
    Console.WriteLine($"Not enought energy! Game ends with {battlesWon} won battles and {initialEnergy} energy");
}
