int[] number = Console.ReadLine()
    .Split(' ')
    .Select(int.Parse)
    .ToArray();

for (int i = 0; i < number.Length; i++)
{
    bool isTop = true;
    for (int j = i + 1; j < number.Length; j++)
    {
        if (number[i] <= number[j])
        {
            isTop = false; 
            break;

        }
    }
    if (isTop)
    {
        Console.Write($"{number[i]} ");
    }
}
