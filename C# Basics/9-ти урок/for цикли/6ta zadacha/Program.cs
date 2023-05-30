using System;
internal class Program
{
    private static void Main(string[] args)
    {
        string word = Console.ReadLine();
        int sum = 0;


        for (int index = 0; index < word.Length; index++)
        {
            char currentSymbol = word[index];

            switch (currentSymbol)
            {
                case 'a': sum += 1; break;
                case 'e': sum += 2; break;
                case 'i': sum += 3; break;
                case 'o': sum += 4; break;
                case 'u': sum += 5; break;
                default:
                    break;
            }
        }
        Console.WriteLine(sum);

    }
}