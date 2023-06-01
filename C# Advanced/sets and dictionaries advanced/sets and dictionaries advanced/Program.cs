List<double> list = Console.ReadLine()
    .Split(" ")
    .Select(double.Parse)
    .ToList();

Dictionary<double, int> result = new Dictionary<double, int>();

foreach (var number in list)
{
    if (!result.ContainsKey(number))
    {
        result.Add(number, 0);
    }

    result[number]++;
}

foreach (var number in result)
{
    Console.WriteLine($"{number.Key} - {number.Value} times");
}
