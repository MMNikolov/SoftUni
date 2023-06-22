using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant
{
    internal class Fish : MainDish
    {
        private const double Grams = 22;
        public Fish(string name, decimal price) : base(name, price, Grams)
        {
        }
    }
}
