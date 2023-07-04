using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BorderControl
{
    public class Robot : IIndividual
    {
        public Robot(string name, string id)
        {
            Name = name;
            Id = id;
        }

        public string Name { get; set; }

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
