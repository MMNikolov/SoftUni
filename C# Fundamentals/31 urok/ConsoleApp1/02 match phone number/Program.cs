using System.Text.RegularExpressions;

string input = Console.ReadLine();

string pattern = @"\+359( |-)2\1\d{3}\1\d{4}\b";

MatchCollection matchCollection = Regex.Matches(input, pattern);

Console.WriteLine(string.Join(", ", matchCollection));
