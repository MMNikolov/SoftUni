namespace _08.Threeuple
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string[] personTokens = Console.ReadLine()
                .Split(" ", StringSplitOptions.RemoveEmptyEntries);

            string[] drinkTokens = Console.ReadLine()
                .Split(" ", StringSplitOptions.RemoveEmptyEntries);

            string[] bankTokens = Console.ReadLine()
                .Split(" ", StringSplitOptions.RemoveEmptyEntries);

            Threeuple<string, string, string> person = new Threeuple<string, string, string>
            {
                Item1 = $"{personTokens[0]} {personTokens[1]}",
                Item2 = personTokens[2],
                Item3 = personTokens[3]
            };
            Threeuple<string, int, bool> drinks = new Threeuple<string, int, bool>
            {
                Item1 = drinkTokens[0],
                Item2 = int.Parse(drinkTokens[1]),
                Item3 = drinkTokens[2] == "drunk"
            };
            Threeuple<string, double, string> bank = new Threeuple<string, double, string>
            {
                Item1 = bankTokens[0],
                Item2 = double.Parse(bankTokens[1]),
                Item3 = bankTokens[2]
            };

            Console.WriteLine(person);
            Console.WriteLine(drinks);
            Console.WriteLine(bank);
        }
    }
}