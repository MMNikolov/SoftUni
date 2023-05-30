int count = int.Parse(Console.ReadLine());

int sum = 0;

for (int i = 0; i < count; i++)
{
    int character = char.Parse(Console.ReadLine());
    sum += character;
}

Console.WriteLine($"The sum equals: {sum}");
