namespace ConsoleApp3
{
    internal class Program
    {
        static void Main(string[] args)
        {
            int people = int.Parse(Console.ReadLine());
            int[] array = Console.ReadLine()
                .Split(' ', StringSplitOptions.RemoveEmptyEntries)
                .Select(int.Parse)
                .ToArray();

            for (int i = 0; i < array.Length; i++)
            {
                for (int j = array[i]; j < 4; j++)
                {
                    if (people >= 4)
                    {
                        array[i]++;
                        people--;
                    }
                    else if (people < 4)
                    {
                        array[i] = people;
                    }
                }
            }

            if (people == 0)
            {
                Console.WriteLine($"The lift has empty spots!");
                Console.WriteLine(array);
            }
            else if (people > 0)
            {
                Console.WriteLine($"There isn't enough space! {people} people in a queue!");
                Console.WriteLine(array);
            }  
        }
    }
}