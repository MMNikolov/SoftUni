using System.Diagnostics.Contracts;
using System.Text.RegularExpressions;

var input = Console.ReadLine();
var parking = new HashSet<string>();

while (input != "END")
{
    var inputParams = Regex.Split(input, ", ");

	if (inputParams[0] == "IN")
	{
		parking.Add(inputParams[1]);
	}
	else if (inputParams[0] == "OUT")
	{
		parking.Remove(inputParams[1]);
	}

	input = Console.ReadLine();
}

foreach (var car in parking)
{
	if (parking.Any())
	{
		Console.WriteLine(car);
	}
}

if (!parking.Any())
{
    Console.WriteLine("Parking Lot is Empty");
}
