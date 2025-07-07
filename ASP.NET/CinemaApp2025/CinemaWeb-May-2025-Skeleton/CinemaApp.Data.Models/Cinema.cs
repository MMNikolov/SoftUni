using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CinemaApp.Data.Models
{
    public class Cinema
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = null!;
        public string Location { get; set; } = null!;
        public bool IsDeleted { get; set; }
        public ICollection<CinemaMovie> CinemaMovies { get; set; } = new List<CinemaMovie>();
        public ICollection<Ticket> Tickets { get; set; } = new List<Ticket>();
    }
}
