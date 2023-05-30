int n = int.Parse(Console.ReadLine());

int[] num = new int[n];

for (int i = 0; i < num.Length; i++)
{
    int currentNumber = int.Parse(Console.ReadLine());
    num[i] = currentNumber;
}

for (int i = num.Length - 1; i >= 0; i--)
{
    Console.Write(num[i] + " ");
}
