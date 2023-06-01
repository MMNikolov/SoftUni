int n = int.Parse(Console.ReadLine());

HashSet<string> names = new();

for (int i = 0; i < n; i++)
{
    names.Add(Console.ReadLine());
}

foreach (var item in names)
{
    Console.WriteLine(item);
}