string[] arguments = Console.ReadLine().Split();

Console.WriteLine(GetCharacterCodeSum(arguments.First(), arguments.Last()));

static int GetCharacterCodeSum(string first, string second)
{
    int sum = 0;
    int maxLength = Math.Max(first.Length, second.Length);
    
    for (int i = 0; i < maxLength; i++)
    {
        int charCode1 = i < first.Length ? first[i] : 1;
        int charCode2 = i < second.Length ? second[i] : 1;

        sum += charCode1 * charCode2;
    }

    return sum;
}
