using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _01.BoxOfString
{
    public class Box<T>
    {
        public T Value { get; set; }
        public Box(T text)
        {
            Value = text;
        }
        public void ToString()
        {
            Console.WriteLine($"{Value.GetType()}: {Value}");
        }
    }
}
