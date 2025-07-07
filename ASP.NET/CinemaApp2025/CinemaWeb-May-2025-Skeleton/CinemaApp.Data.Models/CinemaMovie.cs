using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CinemaApp.Data.Models
{
    public class CinemaMovie
    {
        public Guid MovieId { get; set; }
        public Movie Movie { get; set; } = null!;
        public Guid CinemaId { get; set; }
        public Cinema Cinema { get; set; } = null!;
        public int AvailableTickets { get; set; }
        public bool IsDeleted { get; set; }
        public string? Showtimes { get; set; }
    }
}
