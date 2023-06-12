namespace _02.BoxOfInteger
{
    internal class Program
    {
        static void Main(string[] args)
        {
            int n = int.Parse(Console.ReadLine());

            for (int i = 0; i < n; i++)
            {
                int text = int.Parse(Console.ReadLine());
                Box<int> box = new Box<int>(text);

                box.ToString();
            }
        }
    }
}