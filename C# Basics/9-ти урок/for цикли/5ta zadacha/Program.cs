using System;
internal class Program
{
    private static void Main(string[] args)
    {
        string word = Console.ReadLine();

        for (int index = 0; index < word.Length; index++)
        {
            Console.WriteLine(word[index]);
        }

    }
}