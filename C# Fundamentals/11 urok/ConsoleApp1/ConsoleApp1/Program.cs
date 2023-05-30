string[] days = { "Monday", "Tuesday", "Wednesday", "Thursday", "Friday", "Saturday", "Sunday" };

int intAsDaysOfWeek = int.Parse(Console.ReadLine());

if (intAsDaysOfWeek >= 1 && intAsDaysOfWeek <= 7)
{
    Console.WriteLine(days[intAsDaysOfWeek - 1]);
}
else
{
    Console.WriteLine("Invalid day!");
}


