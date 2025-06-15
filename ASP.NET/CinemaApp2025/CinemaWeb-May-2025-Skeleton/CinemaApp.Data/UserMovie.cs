using CinemaApp.Data.Models;
using Microsoft.AspNetCore.Identity;

namespace CinemaApp.Web.Areas.Identity
{
    public class UserMovie
    {
        public string UserId { get; set; } = null!;
        public virtual IdentityUser User { get; set; } = null!;

        public Guid MovieId { get; set; }
        public virtual Movie Movie { get; set; } = null!;
    }
}
