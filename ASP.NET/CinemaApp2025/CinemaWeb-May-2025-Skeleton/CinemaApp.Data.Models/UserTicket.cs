using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace CinemaApp.Data.Models
{
    public class UserTicket
    {
        public string UserId { get; set; } = null!;
        public IdentityUser User { get; set; } = null!;
        public Guid TicketId { get; set; }
        public Ticket Ticket { get; set; } = null!;
    }
}
