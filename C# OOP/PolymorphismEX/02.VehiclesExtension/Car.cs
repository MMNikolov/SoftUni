using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _02.VehiclesExtension
{
    public class Car : Vehicle
    {
        private const double CONSUMPTION_INCREASE = 0.9;

        public Car(double fuelQuantity, double fuelConsumption, double tankCapacity)
            : base(fuelQuantity, fuelConsumption + CONSUMPTION_INCREASE, tankCapacity)
        {
        }
    }
}
