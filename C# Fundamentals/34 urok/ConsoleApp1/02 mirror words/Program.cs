using System.Text.RegularExpressions;

string pattern = @"(=|/)[A-Z][A-Za-z]{2,}\1";

string input = Console.ReadLine();

MatchCollection matchCollection = Regex.Matches(input, pattern);

List<string> matches = new List<string>();
int travelPoints = 0;

foreach (Match match in matchCollection)
{
    string value = match.Value.Trim('=').Trim('/');
    matches.Add(value);

    travelPoints += value.Length;
}

Console.WriteLine($"Destinations: {string.Join(", ", matches)}");
Console.WriteLine($"Travel Points: {travelPoints}");
