int[] array = Console.ReadLine().Split().Select(int.Parse).ToArray();

int longestSequenceStart = 0;
int longestSequenceLength = 0;

for (int i = 1; i < array.Length; i++)
{
    int currentSequence = i - 1;
    int currentSequenceLength = 1;

    while (i < array.Length && array[i] == array[i - 1])
    {
        currentSequenceLength++;
        i++;
    }

    if (currentSequenceLength > longestSequenceLength)
    {
        longestSequenceLength = currentSequenceLength;
        longestSequenceStart = currentSequence;
    }
}

for (int i = longestSequenceStart; i < longestSequenceStart + longestSequenceLength; i++)
{
    Console.Write($"{array[i]} ");
}
