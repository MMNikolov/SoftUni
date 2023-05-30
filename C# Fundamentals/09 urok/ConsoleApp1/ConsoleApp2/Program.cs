int a = int.Parse(Console.ReadLine());

int sum = 0;


while (a > 0)
{ 
    int b = a % 10;
        a /= 10;
    sum += b;

}

Console.WriteLine(sum);
