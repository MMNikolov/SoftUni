namespace _03_the_angry_catt
{
    internal class Program
    {
        static void Main(string[] args)
        {
            int[] array = Console.ReadLine()
                .Split(", ")
                .Select(int.Parse)
                .ToArray();
            int entry = int.Parse(Console.ReadLine());
            string type = Console.ReadLine();

            if (type == "cheap")
            {
                for (int i = entry + 1; i < array.Length; i++)
                {

                }
            }
        }
    }
}