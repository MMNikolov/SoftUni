namespace _02.EnterNumbers
{
    internal class Program
    {
        static void Main(string[] args)
        {
            List<int> numbers = new List<int>();

            while (numbers.Count < 10)
            {
                try
                {
                    if (!numbers.Any())
                    {
                        numbers.Add(ReadNumber(1, 100));
                    }
                    else
                    {
                        numbers.Add(ReadNumber(numbers.Max(), 100));
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }

            Console.WriteLine(String.Join(", ", numbers));
        }

        static int ReadNumber(int beggining, int end)
        {
            string input = Console.ReadLine();
            int num;

            try
            {
                num = int.Parse(input);
            }
            catch (FormatException)
            {
                throw new FormatException("Invalid Number!");
            }

            if (num <= beggining || num >= end)
            {
                throw new ArgumentException($"Your number is not in range {beggining} - 100!");
            }

            return num;
        }
    }
}