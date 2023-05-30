using System;
internal class Program
{
    private static void Main(string[] args)
    {
        int hours = int.Parse(Console.ReadLine());
        string day = Console.ReadLine();

        if (hours >= 10 && hours <= 18)
        {
            if (day == "Monday" || day == "Tuesday" || day == "Wednesday" || day == "Thursday" || day == "Friday" || day == "Saturday")
            {
                Console.WriteLine("open");
            }
            else if (day == "Sunday")
            {
                Console.WriteLine("closed");
            }
        }
        else if (hours < 10 || hours > 18)
        {
            if (day == "Monday" || day == "Tuesday" || day == "Wednesday" || day == "Thursday" || day == "Friday" || day == "Saturday")
            {
                Console.WriteLine("closed");
            }
            else if (day == "Sunday")
            {
                Console.WriteLine("closed");
            }

        }
    }
}