using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace _05.CountMethodStrings
{
    public class Box<T> where T : IComparable<T>
    {
        private List<T> items = new();

        public void Add(T item)
        {
            items.Add(item);
        }

        public int Count(T itemToCompare)
        {
            int count = 0;

            foreach (var item in items)
            {
                if (item.CompareTo(itemToCompare) > 0)
                {
                    count++;
                }
            }

            return count;
        }

        public override string ToString()
        {
            StringBuilder sb = new();

            foreach (T item in items)
            {
                sb.AppendLine($"{typeof(T)}: {item}");
            }

            return sb.ToString().TrimEnd();
        }
    }
}
