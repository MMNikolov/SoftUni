while (true)
{
    string command = Console.ReadLine();
    if (command == "end")
    {
        break;
    }

    string reversed = "";

    for (int i = command.Length - 1; i >= 0; i--)
    {
        reversed += command[i];
    }

    Console.WriteLine($"{command} = {reversed}");
}
