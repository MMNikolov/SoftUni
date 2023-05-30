using System;
internal class Program
{
    private static void Main(string[] args)
    {
        double averageSpeed = double.Parse(Console.ReadLine());
        double litters = double.Parse(Console.ReadLine());
        int distance = 384400;

        int distanceAll = distance * 2;
        double timeForActions =(distanceAll / averageSpeed);
        timeForActions = Math.Ceiling(timeForActions);
        double allTime = timeForActions + 3;
        double fuel = (litters * distanceAll) / 100;

        Console.WriteLine(allTime);
        Console.WriteLine(fuel);


    }
}