using Vehicles.Core;
using Vehicles.Core.Interfaces;
using Vehicles.Factories;
using Vehicles.IO;

namespace Vehicles.Models
{
    public class StartUp
    {
        static void Main(string[] args)
        {
            IEngine engine = new Engine(new ConsoleReader(), new ConsoleWriter(), new VehicleFactory());
            engine.Run();
        }
    }
}