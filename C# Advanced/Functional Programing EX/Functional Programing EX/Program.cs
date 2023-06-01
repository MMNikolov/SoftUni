
Action<string[]> print = (strings) =>
Console.WriteLine(String.Join(Environment.NewLine, strings));

string[] strings = Console.ReadLine().Split(' ', StringSplitOptions.RemoveEmptyEntries); 
print(strings);

