string start = Console.ReadLine();

string command = Console.ReadLine();

while (command != "Finish")
{
    string[] tokens = command.Split(" ");
    string action = tokens[0];

    if (action == "Replace")
    {
        string currentChar = tokens[1];
        string newChar = tokens[2];

        start = start.Replace(currentChar, newChar);

        Console.WriteLine(start);
    }
    else if (action == "Cut")
    {
        int startIndex = int.Parse(tokens[1]);
        int endIndex = int.Parse(tokens[2]);

        if (startIndex >= 0 && endIndex < start.Length)
        {
            string substring = start.Substring(startIndex, endIndex - startIndex + 1);
            start = start.Remove(startIndex, substring.Length);
            //start += substring;

            Console.WriteLine(start);
        }
        else
        {
            Console.WriteLine("Invalid indices!");
        }
    }
    else if (action == "Make")
    {
        if (tokens[1] == "Upper")
        {
            start = start.ToUpper();
        }
        else if (tokens[1] == "Lower")
        {
            start = start.ToLower();
        }

        Console.WriteLine(start);

    }
    else if (action == "Check")
    {
        string check = tokens[1];

        if (start.Contains(check))
        {
            Console.WriteLine($"Message contains {check}");
        }
        else if (!start.Contains(check))
        {
            Console.WriteLine($"Message doesn't contain {check}");
        }

    }
    else if (action == "Sum")
    {
        int startIndex = int.Parse(tokens[1]);
        int endIndex = int.Parse(tokens[2]);
        if (startIndex >= 0 && endIndex < start.Length)
        {
            int lenght = endIndex - startIndex;

            char[] substring = (start.Substring(startIndex, lenght + 1)).ToCharArray();
            int totalSum = 0;

            foreach (var ch in substring)
            {
                totalSum += ch;
            }

            Console.WriteLine(totalSum);
        }
        else
        {
            Console.WriteLine("Invalid indices!");
        }
    }

    command = Console.ReadLine();
}