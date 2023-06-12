namespace _05.CountMethodStrings
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Box<string> box = new Box<string>();

            int count = int.Parse(Console.ReadLine());

            for (int i = 0; i < count; i++)
            {
                string item = Console.ReadLine();

                box.Add(item);
            }

            string itemToCompare = Console.ReadLine();

            Console.WriteLine(box.Count(itemToCompare));

        }
    }
}
