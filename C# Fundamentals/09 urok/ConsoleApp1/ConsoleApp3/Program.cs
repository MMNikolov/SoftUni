int a = int.Parse(Console.ReadLine());
int b = int.Parse(Console.ReadLine());

int number = a / b;

if (a % b != 0)
{
    number++;
}

Console.WriteLine(number);
 