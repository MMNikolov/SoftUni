namespace _02.Collection
{
    internal class Program
    {
        static void Main(string[] args)
        {
            List<string> items = Console.ReadLine()
                .Split(" ", StringSplitOptions.RemoveEmptyEntries)
                .Skip(1)
                .ToList();

            Collection<string> collection = new(items);

            string command;
            while ((command = Console.ReadLine()) != "END")
            {
                switch (command)
                {
                    case "Move":
                        Console.WriteLine(collection.Move());
                        break;
                    case "HasNext":
                        Console.WriteLine(collection.HasNext());
                        break;
                    case "Print":
                        try
                        {
                            collection.Print();
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine(ex.Message);
                        }
                        break;
                    case "PrintAll":
                        foreach (var item in collection)
                        {
                            Console.Write($"{item} ");
                        }
                        Console.WriteLine();
                        break;


                    default:
                        break;
                }
            }


        }
    }
}