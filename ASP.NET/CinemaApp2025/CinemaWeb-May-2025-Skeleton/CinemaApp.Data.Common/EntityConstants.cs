using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CinemaApp.Data.Common
{
    public static class EntityConstants
    {
        public static class Movie
        {
            public const int TitleMaxLength = 100;
            public const int GenreMaxLength = 50;
            public const int DirectorMaxLength = 100;
            public const int DescriptionMaxLength = 1000;
            public const int DurationMin = 1;
            public const int DurationMax = 300; // Assuming duration is in minutes, this allows for a maximum of 5 hours
        }
    }
}
