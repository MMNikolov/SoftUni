using System.Text.RegularExpressions;

int n = int.Parse(Console.ReadLine());

List<int> numbers = new List<int>();

string pattern = @"^(%|\$)(?<command>[A-Za-z]{2,})\1(: )(\[(?<numbers>[0-9]{2,3})\]\|{1,1})+$";

for (int i = 0; i < n; i++)
{
    string input = Console.ReadLine();

    MatchCollection matchCollection = Regex.Matches(input, pattern);

    if (matchCollection.Any())
    {
        foreach (Match match in matchCollection)
        {
            string firstWord = match.Groups["command"].Value;

            string[] value = match.Value.Split(" ");
            string[] stringNumbers = value[1].Split("|");

            foreach (var num in stringNumbers)
            {
                if (string.IsNullOrEmpty(num))
                {
                    continue;
                }
                string numberWithoutFirst = num.Trim('[');
                string clearNumber = numberWithoutFirst.Trim(']');
                numbers.Add(int.Parse(clearNumber));
            }
            if (numbers.Count > 3)
            {
                Console.WriteLine("Valid message not found!");
                continue;
            }
            if (numbers.Count < 2)
            {
                Console.WriteLine("Valid message not found!");
                continue;

            }
            string wordASCII = "";
            foreach (var number in numbers)
            {
                wordASCII += (char)(number);
            }

            Console.WriteLine($"{firstWord}: {wordASCII}");
            numbers.Clear();
        }
    }
    else
    {
        Console.WriteLine("Valid message not found!");
    }
}

