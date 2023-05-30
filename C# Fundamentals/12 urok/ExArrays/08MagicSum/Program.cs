int[] array = Console.ReadLine()
    .Split()
    .Select(int.Parse)
    .ToArray();

int sum = int.Parse(Console.ReadLine());

for (int i = 0; i < array.Length; i++)
{
    for (int j = i + 1; j < array.Length; j++)
    {
        if (sum == array[i] + array[j])
        {
            Console.WriteLine("{0} {1}", array[i], array[j]);
        }
    }
}