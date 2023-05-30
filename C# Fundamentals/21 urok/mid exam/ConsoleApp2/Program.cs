namespace ConsoleApp2
{
    internal class Program
    {
        static void Main(string[] args)
        {
            int n = int.Parse(Console.ReadLine());
            int numPlayers = int.Parse(Console.ReadLine());
            double groupEnergy = double.Parse(Console.ReadLine());
            double waterPerOnePersone = double.Parse(Console.ReadLine());
            double foodPerOnePersone = double.Parse(Console.ReadLine());

            bool flag = true;
            double totalWater = n * numPlayers * waterPerOnePersone;
            double totalFood = n * numPlayers * foodPerOnePersone;

            for (int i = 1; i <= n; i++)
            {
                double energyLoss = double.Parse(Console.ReadLine());
                if (groupEnergy - energyLoss > 0)
                {
                    groupEnergy -= energyLoss;
                    if (i % 2 == 0)
                    {
                        totalWater -= totalWater * 0.3;
                        groupEnergy += 0.05 * groupEnergy;
                    }

                    if (i % 3 == 0)
                    {
                        totalFood -= totalFood / numPlayers;
                        groupEnergy += 0.1 * groupEnergy;
                    }
                }
                else
                {
                    flag = false;
                    break;
                }
            }

            if (flag)
            {
                Console.WriteLine($"You are ready for the quest. You will be left with - {groupEnergy:f2} energy!");
            }
            else
            {
                Console.WriteLine($"You will run out of energy. You will be left with {totalFood:f2} food and {totalWater:f2} water.");
            }
        }
    }
}