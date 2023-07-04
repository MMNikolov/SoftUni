using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Telephony
{
    public class Smartphone : IPhone, IBrowser
    {
        public void Browse(string link)
        {
            if (link.Any(ch => char.IsDigit(ch)))
            {
                Console.WriteLine("Invalid URL!");
            }
            else
            {
                Console.WriteLine($"Browsing: {link}!");
            }
        }

        public void Call(string number)
        {
            if (number.Any(ch => !char.IsDigit(ch)))
            {
                Console.WriteLine("Invalid number!");
            }
            else
            {
                Console.WriteLine($"Calling... {number}");
            }
        }
    }
}
