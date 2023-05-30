using System;
internal class Program
{
    private static void Main(string[] args)
    {
        string flowersType = Console.ReadLine();
        int flowersCount = int.Parse(Console.ReadLine());
        int budget = int.Parse(Console.ReadLine());

        double flowersPrice = 0;

        switch (flowersType)
        {
            case "Roses":
                flowersPrice = 5;
                    break;
            case "Dahlias":
                flowersPrice = 3.8;
                break;
            case "Tulips":
                flowersPrice = 2.8;
                break;
            case "Narcissus":
                flowersPrice = 3;
                break;
            case "Gladiolus":
                flowersPrice = 2.5;
                break;
            default:
                break;
        }
        
        double totalSum = flowersCount * flowersPrice

        if (flowersType == "Roses" && flowersCount > 80)
        {
            totalSum = totalSum * 0.9;

        }
        else if (flowersType == "Dahlias" && flowersCount > 90)
        {
            totalSum *= 0.85;
        }
        else if (flowersType == "Tulips" &&)
        {

        }

    }
}