
int n = int.Parse(Console.ReadLine());
Dictionary<string, int> mileageInfo = new Dictionary<string, int>();
Dictionary<string, int> fuelInfo = new Dictionary<string, int>();

for (int i = 0; i < n; i++)
{
    string[] input = Console.ReadLine().Split("|", StringSplitOptions.RemoveEmptyEntries);
    string car = input[0];
    int mileage = int.Parse(input[1]);
    int fuel = int.Parse(input[2]);
    mileageInfo.Add(car, mileage);
    fuelInfo.Add(car, fuel);
}

while (true)
{
    string[] input = Console.ReadLine().Split(" : ", StringSplitOptions.RemoveEmptyEntries);
    string action = input[0];

    if (action == "Stop")
    {
        break;
    }
    string car = input[1];
    if (action == "Drive")
    {
        int distance = int.Parse(input[2]);
        int fuel = int.Parse(input[3]);
        if (fuel > fuelInfo[car])
        {
            Console.WriteLine("Not enough fuel to make that ride");
        }
        else
        {
            mileageInfo[car] += distance;
            fuelInfo[car] -= fuel;
            Console.WriteLine($"{car} driven for {distance} kilometers. {fuel} liters of fuel consumed.");
            if (mileageInfo[car] >= 100000)
            {
                Console.WriteLine($"Time to sell the {car}!");
                mileageInfo.Remove(car);
                fuelInfo.Remove(car);
            }

        }

    }
    else if (action == "Refuel")
    {
        int fuel = int.Parse(input[2]);
        if (fuel + fuelInfo[car] > 75)
        {
            fuel = 75 - fuelInfo[car];
        }
        fuelInfo[car] += fuel;
        Console.WriteLine($"{car} refueled with {fuel} liters");
    }
    else if (action == "Revert")
    {
        int mileage = int.Parse(input[2]);

        if (mileageInfo[car] - mileage < 10000)
        {
            mileageInfo[car] = 10000;
        }
        else
        {
            mileageInfo[car] -= mileage;
            Console.WriteLine($"{car} mileage decreased by {mileage} kilometers");
        }
    }
}
foreach (var item in mileageInfo)
{
    int fuel = fuelInfo[item.Key];
    Console.WriteLine($"{item.Key} -> Mileage: {item.Value} kms, Fuel in the tank: {fuel} lt.");
}
