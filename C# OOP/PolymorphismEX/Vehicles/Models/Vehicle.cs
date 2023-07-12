using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vehicles.Models.Interfaces;

namespace Vehicles.Models
{
    public abstract class Vehicle : IVehicle
    {
        private double increasedConsumption;
        public Vehicle(double fuelQuantity, double fuelConsumtion, double increasedConsumption)
        {
            FuelQuantity = fuelQuantity;
            FuelConsumtion = fuelConsumtion;
            this.increasedConsumption = increasedConsumption;
        }

        public double FuelQuantity { get; private set; }
        public double FuelConsumtion { get; private set; }

        public string Drive(double distance)
        {
            double consumption = FuelConsumtion + increasedConsumption;

            if (FuelQuantity < distance * consumption)
            {
                throw new ArgumentException($"{this.GetType().Name} needs refueling");
            }

            FuelQuantity -= distance * consumption;

            return $"{this.GetType().Name} travelled {distance} km";
        }

        public virtual void Refuel(double amount)
        {
            FuelQuantity += amount;
        }

        public override string ToString()
        {
            return $"{this.GetType().Name}: {FuelQuantity:f2}";
        }
    }
}
