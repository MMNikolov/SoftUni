int iterations = int.Parse(Console.ReadLine());

byte capacity = 255;
byte sum = 0;

for (int i = 0; i < iterations; i++)
{
    short liters = short.Parse(Console.ReadLine());
	if (liters < capacity - sum)
	{
		sum += (byte)liters;
	}
	else
	{
		Console.WriteLine("Insufficient capacity!");
	}
	
}
Console.WriteLine(sum);