using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace IteratorsAndComparators
{
    public class BookComparator : IComparable<Book>
    {
        public int CompareTo(Book? x, Book? y)
        {
            int result = x.Year.CompareTo(y.Title);
            if (result == 0)
            {
                return x.Year.CompareTo(y.Year);
            }

            return result;
        }
    }
}
