Dictionary<string, Dictionary<string, int>> colorsAndDress = new Dictionary<string, Dictionary<string, int>>();

int n = int.Parse(Console.ReadLine());

for (int i = 0; i < n; i++)
{
    string[] input = Console.ReadLine().Split(new string[] { " -> ", "," }, StringSplitOptions.RemoveEmptyEntries);
    string color = input[0];

    if (!colorsAndDress.ContainsKey(color))
    {
        colorsAndDress.Add(color, new Dictionary<string, int>());
    }

    for (int j = 1; j < input.Length; j++)
    {
        if (!colorsAndDress[color].ContainsKey(input[j]))
        {
            colorsAndDress[color].Add(input[j], 0);
        }
        colorsAndDress[color][input[j]]++;
    }
}

string[] serching = Console.ReadLine().Split(" ", StringSplitOptions.RemoveEmptyEntries);
string serchingColor = serching[0];
string serchingDress = serching[1];

foreach (var colors in colorsAndDress)
{
    Console.WriteLine($"{colors.Key} clothes:");
    foreach (var dreses in colors.Value)
    {
        if (serchingColor == colors.Key && serchingDress == dreses.Key)
        {
            Console.WriteLine($"* {dreses.Key} - {dreses.Value} (found!)");
        }
        else
        {
            Console.WriteLine($"* {dreses.Key} - {dreses.Value}");
        }
    }
}