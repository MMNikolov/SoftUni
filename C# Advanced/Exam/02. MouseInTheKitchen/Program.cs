int[] dimentions = Console.ReadLine()
    .Split(",", StringSplitOptions.RemoveEmptyEntries)
    .Select(int.Parse)
    .ToArray();

char[,] matrix = new char[dimentions[0], dimentions[1]];

int rows = dimentions[0];
int cols = dimentions[1];

int myRow = -1;
int myCol = -1;

int firstRow = -1;
int firstCol = -1;

int cheeseTouched = 0;

for (int i = 0; i < rows; i++)
{
    string rowString = Console.ReadLine();
    char[] row = rowString.ToCharArray();

    for (int j = 0; j < cols; j++)
    {
        matrix[i, j] = row[j];
        if (matrix[i, j] == 'M')
        {
            firstRow = i;
            firstCol = j;
            myRow = i;
            myCol = j;
            matrix[i, j] = 'M';
        }
    }
}

string direction = Console.ReadLine();

while (true)
{
    if ((direction == "left" && myCol == 0) ||
        (direction == "right" && myCol == matrix.GetLength(1) - 1) ||
        (direction == "up" && myRow == 0) ||
        (direction == "down" && myRow == matrix.GetLength(0) - 1))
    {

        Console.WriteLine("No more cheese for tonight!");
        break;
    }
    switch (direction)
    {
        case "left":
            if (matrix[myRow, myCol - 1] == '@')
            {
                direction = Console.ReadLine();
                continue;
                
            }
            break;
        case "right":
            if (matrix[myRow, myCol + 1] == '@')
            {
                direction = Console.ReadLine();
                continue;
            }
            break;
        case "up":
            if (matrix[myRow - 1, myCol] == '@')
            {
                direction = Console.ReadLine();
                continue;
            }
            break;
        case "down":
            if (matrix[myRow + 1, myCol] == '@')
            {
                direction = Console.ReadLine();
                continue;
            }
            break;
    }

    switch (direction)
    {
        case "left":
            myCol--;
            break;
        case "right":
            myCol++;
            break;
        case "up":
            myRow--;
            break;
        case "down":
            myRow++;
            break;
    }

    if (matrix[myRow, myCol] == 'C')
    {
        cheeseTouched++;
        matrix[myRow, myCol] = '*';

        bool flag = true;

        foreach (var item in matrix)
        {
            if (item == 'C')
            {
                flag = false;
            }
        }

        if (flag)
        {
            Console.WriteLine("Happy mouse! All the cheese is eaten, good night!");
            matrix[myRow, myCol] = 'M';
            break;
        }
    }

    if (matrix[myRow, myCol] == 'T')
    {
        Console.WriteLine("Mouse is trapped!");
        break;
    }

    if (direction == "danger")
    {
        Console.WriteLine("Mouse will come back later!");
        break;
    }

    direction = Console.ReadLine();

    

}
matrix[firstRow, firstCol] = '*';
matrix[myRow, myCol] = 'M';
Print2DArray(matrix);

static void Print2DArray<T>(T[,] matrix)
{
    for (int i = 0; i < matrix.GetLength(0); i++)
    {
        for (int j = 0; j < matrix.GetLength(1); j++)
        {
            Console.Write(matrix[i, j]);
        }
        Console.WriteLine();
    }
}
