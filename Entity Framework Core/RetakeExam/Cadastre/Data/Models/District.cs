using Cadastre.Data.Enumerations;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cadastre.Data.Models
{
    public class District
    {
        public District()
        {
            Properties = new List<Property>();
        }

        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(80)]
        public string Name { get; set; } = null!;

        [Required]
        [MaxLength(8)]
        public string PostalCode { get; set; } = null!;

        [Required]
        public Region Region { get; set; }

        public virtual ICollection<Property> Properties { get; set; }
    }
}
