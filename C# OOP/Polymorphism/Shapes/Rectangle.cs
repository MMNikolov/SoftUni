using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Shapes
{
    public class Rectangle : Shape
    {

        public Rectangle(int x, int y)
        {
            this.X = x;
            this.Y = y;
        }

        public int X { get; private set; }

        public int Y { get; private set; }

        public override double CalculateArea()
        {
            return this.X * this.Y;
        }

        public override double CalculatePerimeter()
        {
            return 2 * this.X + 2 * this.Y;
        }
    }
}
