Dictionary<string, int> resoursAmount = new Dictionary<string, int>();
while (true)
{
    string command = Console.ReadLine();
    if (command == "stop")
    {
        break;
    }
    int quantity = int.Parse(Console.ReadLine());


    if (resoursAmount.ContainsKey(command))
    {
        resoursAmount[command] += quantity;
    }
    else
    {
        resoursAmount.Add(command, quantity);
    }
}

foreach (var item in resoursAmount)
{
    Console.WriteLine($"{item.Key} -> {item.Value}");
}