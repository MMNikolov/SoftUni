int n = int.Parse(Console.ReadLine());

Stack<int> stack = new Stack<int>();

List<int> list = new List<int>();

for (int i = 0; i < n; i++)
{
    int[] givenNum = Console.ReadLine()
        .Split(" ", StringSplitOptions.RemoveEmptyEntries)
        .Select(int.Parse)
        .ToArray();

    int num = givenNum[0];

    switch (num)
    {
        case 1:
            int secondNum = givenNum[1];
            stack.Push(secondNum);
            break;
        case 2:
            if (stack.Count > 0)
            {
                stack.Pop();
            }
            break;
        case 3:
            if (stack.Count > 0)
            {
                list = stack.OrderBy(x => x).ToList();
                Console.WriteLine(list[list.Count - 1]);
            }
            break;
        case 4:
            if (stack.Count > 0)
            {
                list = stack.OrderBy(x => x).ToList();
                Console.WriteLine(list[0]);
            }
            break;
        default:
            break;
    }
}

Console.WriteLine(string.Join(", ", stack));
