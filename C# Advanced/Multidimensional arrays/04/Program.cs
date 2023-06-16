int rows = int.Parse(Console.ReadLine());
int cols = rows;

char[,] matrix = new char[rows, cols];

for (int row = 0; row < rows; row++)
{
    char[] input = Console.ReadLine().ToCharArray();

    for (int col = 0; col < cols; col++)
    {
        matrix[row, col] = input[col];
    }
}

char searching = char.Parse(Console.ReadLine());
bool isItRight = false;

for (int row = 0; row < rows; row++)
{
    for (int col = 0; col < cols; col++)
    {
        if (searching == matrix[row, col])
        {
            Console.WriteLine($"({row}, {col})");
            isItRight = true;
            return;
        }
    }
}

if (!isItRight)
{
    Console.WriteLine($"{searching} does not occur in the matrix");
}