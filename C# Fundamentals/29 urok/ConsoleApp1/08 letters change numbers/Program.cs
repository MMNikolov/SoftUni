string[] sequences = Console.ReadLine().Split(' ', StringSplitOptions.RemoveEmptyEntries);

double sum = 0;

foreach (string sequence in sequences)
{
    double result = 0;

    char firstChar = sequence.First();
    char lastChar = sequence.Last();

    double firstOperand = double.Parse(sequence.Substring(1, sequence.Length - 2));
    double secondOperand = GetLetterPositionInTheAlphabet(firstChar);
    double thirdOperand = GetLetterPositionInTheAlphabet(lastChar);

    if (char.IsUpper(firstChar))
    {
        result = firstOperand / secondOperand;
    }
    else
    {
        result = firstOperand * secondOperand;
    }

    if (char.IsUpper(lastChar))
    {
        result -= thirdOperand;
    }
    else
    {
        result += thirdOperand;
    }
    sum += result;


}

Console.WriteLine($"{sum:f2}");

static int GetLetterPositionInTheAlphabet(char c)
{
    return c.ToString().ToLower().First() - 96;
}
