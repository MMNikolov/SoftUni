Stack<char> text = new Stack<char> ();

string input = Console.ReadLine();

foreach (var item in input)
{
    text.Push(item);
}

while (text.Any())
{
    char result = text.Pop ();
    Console.Write(result);
}
