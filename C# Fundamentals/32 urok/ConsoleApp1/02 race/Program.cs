using System.Text.RegularExpressions;

string[] participants = Console.ReadLine()
    .Split(", ", StringSplitOptions.RemoveEmptyEntries);
Dictionary<string, int> result = new Dictionary<string, int>();

foreach (string participant in participants)
{
    result.Add(participant, 0);
}
string input = Console.ReadLine();

while (input != "end of race")
{
    MatchCollection nameRegexCollection = Regex.Matches(input, @"([A-Za-z]+)");
    MatchCollection distanceRegexCollection = Regex.Matches(input, @"(\d)");

    string name = string.Join(string.Empty, nameRegexCollection);
    if (result.ContainsKey(name))
    {
        result[name] += distanceRegexCollection.Select(x => int.Parse(x.Value)).Sum();
    }
    input = Console.ReadLine();
}
var finalists = result.OrderByDescending(x => x.Value).Take(3);

int counter = 1;
foreach (var finalist in finalists)
{
    string suffix = counter == 1 ? "st" : counter == 2 ? "nd" : "rd";
    Console.WriteLine($"{counter++}{suffix} place: {finalist.Key}");
}
