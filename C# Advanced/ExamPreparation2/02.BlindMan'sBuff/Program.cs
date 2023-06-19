using System.Data;

int[] dimensions = Console.ReadLine()
    .Split(" ",StringSplitOptions.RemoveEmptyEntries)
    .Select(int.Parse)
    .ToArray();

int rows = dimensions[0];
int cols = dimensions[1];

string[,] playground = new string[rows, cols];

int myRow = -1;
int myCol = -1;

int opponentsTouched = 0;
int moves = 0;



for (int i = 0; i < rows; i++)
{
    string[] row = Console.ReadLine()
        .Split(" ",StringSplitOptions.RemoveEmptyEntries);

    for (int j = 0; j < cols; j++)
    {

        playground[i, j] = row[j].ToString();
        if (playground[i, j] == "B")
        {
            myRow = i;
            myCol = j;
            playground[i, j] = "-";
        }
    }
}

string direction;
while ((direction = Console.ReadLine()) != "Finish")
{
    if ((direction == "left" && myCol == 0) ||
        (direction == "right" && myCol == playground.GetLength(1) - 1) ||
        (direction == "up" && myRow == 0) ||
        (direction == "down" && myRow == playground.GetLength(0) - 1))
    {
        continue;
    }
    switch (direction)
    {
        case "left":
            if (playground[myRow, myCol - 1] == "O")
            {
                continue;
            }
            break;
        case "right":
            if (playground[myRow, myCol + 1] == "O")
            {
                continue;
            }
            break;
        case "up":
            if (playground[myRow - 1, myCol] == "O")
            {
                continue;
            }
            break;
        case "down":
            if (playground[myRow + 1, myCol] == "O")
            {
                continue;
            }
            break;
    }

    moves++;
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

    if (playground[myRow, myCol] == "P")
    {
        opponentsTouched++;
        playground[myRow, myCol] = "-";

        if (opponentsTouched == 3)
        {
            break;
        }
    }
}

Console.WriteLine("Game over!");
Console.WriteLine($"Touched opponents: {opponentsTouched} Moves made: {moves}");
