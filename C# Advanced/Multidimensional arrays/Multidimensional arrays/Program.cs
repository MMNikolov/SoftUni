string[] input = Console.ReadLine()
    .Split(", ")
    .ToArray();

int rows = int.Parse(input[0]);
int cols = int.Parse(input[1]);

int[,] matrix = new int[rows, cols];

for (int row = 0; row < rows; row++)
{
    int[] array = Console.ReadLine()
        .Split(", ")
        .Select(int.Parse)
        .ToArray();

    for (int col = 0; col < cols; col++)
    {
        matrix[row, col] = array[col];
    }
}
int sum = 0;
for (int row = 0; row < rows; row++)
{
    for (int col = 0; col < cols; col++)
    {
        sum += matrix[row, col];
    }
}

Console.WriteLine(rows);
Console.WriteLine(cols);
Console.WriteLine(sum);
