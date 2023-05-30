double food = double.Parse(Console.ReadLine()) * 1000;
double hay = double.Parse(Console.ReadLine()) * 1000;
double cover = double.Parse(Console.ReadLine()) * 1000;
double weight = double.Parse(Console.ReadLine()) * 1000;

int days = 1;

for (int i = 1; i < 30; i++)
{
    food -= 300;

    if (days % 2 == 0)
    {
        hay -= food * 0.05;
    }
    if (days % 3 == 0)
    {
        cover -= 0.33 * weight;
    }

    days++;
}

if (food > 0 && hay > 0 && cover > 0)
{
    Console.WriteLine($"Everything is fine! Puppy is happy! Food: {food}, Hay: {hay}, Cover: {cover}.");
}
else
{
    Console.WriteLine($"Merry must go to the pet store!");
}