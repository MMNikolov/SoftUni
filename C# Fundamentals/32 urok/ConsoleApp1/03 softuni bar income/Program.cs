using System.Text;
using System.Text.RegularExpressions;

string input = Console.ReadLine();

string customer = "(?<customer>[A-Z][a-z]+)";
string product = @"(?<product>\w+)";
string count = @"(?<count>\d+)";
string price = @"(?<price>\d+(\.\d+)?)";
string junk = @"[^|$%.]";
string pattern = @$"\%{customer}\%{junk}*\<{product}\>{junk}*\|{count}\|{junk}*?{price}\$";

decimal totalSum = 0;

StringBuilder report = new StringBuilder();

while (input != "end of shift")
{
    MatchCollection matchCollection = Regex.Matches(input, pattern);

	foreach (Match match in matchCollection)
	{
		string customerName = match.Groups["customer"].Value;
		string productName = match.Groups["product"].Value;
		int countVal = int.Parse(match.Groups["count"].Value);
		decimal singlePrice = decimal.Parse(match.Groups["price"].Value);
		decimal clientTotal = countVal * singlePrice;
		totalSum += clientTotal;
		report.AppendLine($"{customerName}: {productName} - {clientTotal:f2}");
	}

	input = Console.ReadLine();
}
Console.Write(report.ToString());
Console.WriteLine($"Total income: {totalSum:f2}");
