int count = int.Parse(Console.ReadLine());

string text = string.Empty;

Stack<string> changes = new Stack<string>();

for (int i = 0; i < count; i++)
{
    string[] tokens = Console.ReadLine()
        .Split(new char[] { ' ' }, 
        StringSplitOptions.RemoveEmptyEntries);

    int command = int.Parse(tokens[0]);

    switch (command)
    {
        case 1:
            changes.Push(text);
            text += tokens[1];
            break;
        case 2:
            changes.Push(text);
            int countToErase = int.Parse(tokens[1]);
            text = text.Remove(text.Length - countToErase);
            break;
        case 3:
            int index = int.Parse(tokens[1]);
            Console.WriteLine(text[index - 1]);
            break;
        case 4:
            text = changes.Pop();
            break;
    }
}