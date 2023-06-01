namespace CarManufacturer
{
    public class StartUp
    {
        static void Main()
        {
            Car car = new Car()
            {
                Make = Console.ReadLine(),
                Model = Console.ReadLine()
            };

            Console.WriteLine($"{car.Make} {car.Model}");

        }
    }
}

