int number = int.Parse(Console.ReadLine());

string bestModel = "";
double biggestVolume = 0;

for (int i = 0; i < number; i++)
{
    string model = Console.ReadLine();
    double radius = double.Parse(Console.ReadLine());
    int height = int.Parse(Console.ReadLine());

    double volume = Math.PI * Math.Pow(radius, 2) * height;
    if (volume > biggestVolume)
    {
        biggestVolume = volume;
        bestModel = model;
    }
}

Console.WriteLine(bestModel);
