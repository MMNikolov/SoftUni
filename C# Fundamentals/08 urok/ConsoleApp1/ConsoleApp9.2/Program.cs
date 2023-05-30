double dul, sh, V = 0;

Console.Write("Length: ");

dul = double.Parse(Console.ReadLine());

Console.Write("Width: ");

sh = double.Parse(Console.ReadLine());

Console.Write("Heigth: ");

V = double.Parse(Console.ReadLine());

V = (dul + sh + V) / 3;

Console.Write($"Pyramid Volume: {V:f2}");