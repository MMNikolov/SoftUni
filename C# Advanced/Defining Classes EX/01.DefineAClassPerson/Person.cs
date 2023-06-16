using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DefiningClasses
{
    public class Person
    {
		private string name;

		public string Name
		{
			get { return this.name; }
			set { this.name = value; }
		}

		private int age;

		public int Age
		{
			get { return this.age; }
			set { this.age = value; }
		}
	}
}
