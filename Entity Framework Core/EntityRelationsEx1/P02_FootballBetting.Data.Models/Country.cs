using System.ComponentModel.DataAnnotations;

namespace P02_FootballBetting.Data.Models
{
    public class Country
    {
        public int CountryId { get; set; }
        [Required]
        [MaxLength(100)]
        public string Name { get; set; }

        public virtual ICollection<Town> Towns { get; set; }
    }
}
