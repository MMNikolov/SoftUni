namespace _01.SquareRoot
{
    internal class Program
    {
        static void Main(string[] args)
        {
            try
            {
                int input = int.Parse(Console.ReadLine());

                if (input < 0)
                {
                    throw new ArithmeticException("Invalid number.");

                }
                double output = Math.Sqrt(input);

                Console.WriteLine(output);
            }
            catch (ArithmeticException ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                Console.WriteLine("Goodbye.");
            }
        }
    }
}