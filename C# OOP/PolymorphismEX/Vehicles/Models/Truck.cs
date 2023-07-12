using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vehicles.Models
{
    public class Truck : Vehicle
    {
        private const double IncreasedConsumtion = 1.6;

        public Truck(double fuelQuantity, double fuelConsumtion) 
            : base(fuelQuantity, fuelConsumtion, IncreasedConsumtion)
        {
        }
    }
}
