using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Person
{
    public class Person
    {
        public Person(string name, int age)
        {
            Name = name;
            Age = age;
        }
        public string Name { get; set; }

        public int Age { get; set; }
        public override string ToString()
        {
            StringBuilder stringBuilder = new();
            stringBuilder.Append(String.Format($"Name: {this.Name}, Age: {this.Age}"));
            
            return stringBuilder.ToString();
        }

    }
}
