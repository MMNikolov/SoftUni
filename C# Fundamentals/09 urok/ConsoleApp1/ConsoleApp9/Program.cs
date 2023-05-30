int source = int.Parse(Console.ReadLine());

int days = 0;
int sum = 0;
int spicePerDay = 0;

while (source >= 100)
{
    days++;
    spicePerDay = source - 26;
    sum += spicePerDay;
    source -= 10;
}

if (days > 0)
{
    sum -= 26;
}

Console.WriteLine(days);
Console.WriteLine(sum);
