using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace BorderControl
{
    public class Human : IIndividual
    {
        public Human(string name, int age, string id)
        {
            Name = name;
            Age = age;
            Id = id;
        }

        public string Name { get; set; }

        public int Age { get; set; }

        public string Id { get; set; }

        public void CheckId(string fakeSubstring)
        {
            if (this.Id.EndsWith(fakeSubstring))
            {
                Console.WriteLine(this.Id);
            }
        }
    }
}
