using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomStack
{
    public class StackOfStrings : Stack<string> 
    {
        Stack<string> stack = new();

        public bool IsEmpty()
        {
            if (stack.Any())
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public Stack<string> AddRange(IEnumerable<string> range)
        {
            foreach (var item in range)
            {
                Push(item);
            }

            return this;
        }
    }
}
