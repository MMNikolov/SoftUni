using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vehicles.Core.Interfaces;
using Vehicles.Factories.Interfaces;
using Vehicles.IO.Interfaces;
using Vehicles.Models;
using Vehicles.Models.Interfaces;

namespace Vehicles.Core
{
    public class Engine : IEngine
    {
        private IReader reader;
        private IWriter writer;
        private IVehicleFactory vehicleFactory;

        private ICollection<IVehicle> vehicles;

        public Engine(IReader reader, IWriter writer, IVehicleFactory vehicleFactory)
        {
            this.reader = reader;
            this.writer = writer;
            this.vehicleFactory = vehicleFactory;

            vehicles = new List<IVehicle>();
        }

        public void Run()
        {
            IVehicle car = CreateVehicle();
            IVehicle truck = CreateVehicle();

            vehicles.Add(car);
            vehicles.Add(truck);

            int commandsCount = int.Parse(Console.ReadLine());

            for (int i = 0; i < commandsCount; i++)
            {
                try
                {
                    ProcessCommand();
                }
                catch (Exception ex)
                {

                    writer.WriteLine(ex.Message);
                }

            }

            foreach (var vehicle in vehicles)
            {
                Console.WriteLine(vehicle);
            }
        }

        private IVehicle CreateVehicle()
        {
            string[] tokens = reader.ReadLine()
                .Split(" ", StringSplitOptions.RemoveEmptyEntries);

            IVehicle vehicle = vehicleFactory
                .Create(tokens[0], double.Parse(tokens[1]), double.Parse(tokens[2]));

            return vehicle;
        }

        private void ProcessCommand()
        {
            string[] commandTokens = reader.ReadLine()
                .Split(" ", StringSplitOptions.RemoveEmptyEntries);

            string command = commandTokens[0];
            string vehicleTypeName = commandTokens[1];

            IVehicle vehicle = vehicles.FirstOrDefault(v => v.GetType().Name == vehicleTypeName);

            if (vehicle is null)
            {
                throw new ArgumentException("Invalid vehicle type");
            }

            if (command == "Drive")
            {
                double distance = double.Parse(commandTokens[2]);
                writer.WriteLine(vehicle.Drive(distance));
            }
            else if (command == "Refuel")
            {
                double amount = double.Parse(commandTokens[2]);
                vehicle.Refuel(amount);
            }
        }
    }
}
