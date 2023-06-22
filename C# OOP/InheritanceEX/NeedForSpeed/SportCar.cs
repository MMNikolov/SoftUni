using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeedForSpeed
{
    public class SportCar : Car
    {
        private const double DefaultFuelConsumption = 10;

        public SportCar(double fuel, int horsePower) : base(fuel, horsePower)
        {
        }
        public override double FuelConsumtion => DefaultFuelConsumption;
    }
}
