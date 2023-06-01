string input =  Console.ReadLine();

Stack<int> indexes = new Stack<int>();

for (int i = 0; i < input.Length; i++)
{
	if (input[i] == '(')
	{
		indexes.Push(i);
	}
	else if (input[i] == ')')
	{
		int indexOfOpenBracket = indexes.Pop();

		for (int j = indexOfOpenBracket; j < i; j++)
		{
			Console.Write(input[j]);
		}

		Console.WriteLine();
	}
}
