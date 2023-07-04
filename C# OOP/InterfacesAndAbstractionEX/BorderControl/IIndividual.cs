using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BorderControl
{
    public interface IIndividual
    {
        string Name { get; }
        string Id { get; }

        void CheckId(string fakeSubstring);
    }
}
