internal class Program
{
    static void Main()
    {
        string text = Console.ReadLine();
        int n = int.Parse(Console.ReadLine());

        string result = "";
        result = NewMethod(text, n, result);

        Console.WriteLine(result);

    }

    private static string NewMethod(string text, int n, string result)
    {
        for (int i = 0; i < n; i++)
        {
            result += text;
        }

        return result;
    }
}
